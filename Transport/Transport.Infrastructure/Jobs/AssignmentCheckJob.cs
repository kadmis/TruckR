using BuildingBlocks.Application.Data;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using BuildingBlocks.Domain;
using MediatR;
using Transport.Infrastructure.InternalCommands.SetAssignmentAsFailed;
using Transport.SignalRClient.Clients.Shared;
using System.Threading;

namespace Transport.Infrastructure.Jobs
{
    [DisallowConcurrentExecution]
    public class AssignmentCheckJob : IJob
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly ITransportHubClient _client;
        private readonly IMediator _mediator;

        public AssignmentCheckJob(
            ISqlConnectionFactory sqlConnection,
            ITransportHubClient client, 
            IMediator mediator)
        {
            _sqlConnection = sqlConnection;
            _client = client;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Work(context.CancellationToken);
        }

        private async Task Work(CancellationToken cancellationToken = default)
        {
            var query = 
                "SELECT " +
                "A.Id, " +
                "A.DriverId, " +
                "A.DispatcherId " +
                "FROM dbo.Assignments AS A " +
                "WHERE " +
                "A.CompletedOn IS NULL " +
                "AND A.FailedOn IS NULL " +
                "AND A.Deadline < @Now";

            try
            {
                var signalrTasks = new List<Task>();
                var internalCommandTasks = new List<Task>();

                var connection = _sqlConnection.GetOpenConnection();

                var assignments = await connection.QueryAsync<UnfinishedAssignment>(query, new { Now = Clock.Now });

                foreach (var assignment in assignments)
                {
                    if(assignment.DriverId.HasValue)
                        signalrTasks.Add(_client.SendAssignmentFailed(
                            assignment.Id, assignment.DriverId.Value, assignment.DispatcherId, cancellationToken));
                    else if(!assignment.DriverId.HasValue)
                        signalrTasks.Add(_client.SendAssignmentExpired(
                            assignment.Id, assignment.DispatcherId, cancellationToken));

                    internalCommandTasks.Add(
                        _mediator.Send(new SetAssignmentAsFailedCommand(assignment.Id), 
                        cancellationToken));
                }

                await Task.WhenAll(internalCommandTasks);
                await Task.WhenAll(signalrTasks);
            }
            catch(Exception ex)
            {
            }
        }

        private class UnfinishedAssignment
        {
            public Guid Id { get; set; }
            public Guid? DriverId { get; set; }
            public Guid DispatcherId { get; set; }
        }
    }

}
