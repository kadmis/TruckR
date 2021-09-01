using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.SignalRClient.Clients.Shared
{
    public class TransportHubClient : ITransportHubClient
    {
        private readonly HubConfiguration _configuration;

        public TransportHubClient(HubConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAssignmentFailed(Guid assignmentId, Guid driverId, Guid dispatcherId, CancellationToken cancellationToken = default)
        {
            await using HubConnection connection = new HubConnectionBuilder()
                .WithUrl(_configuration.Transport)
                .Build();
            await connection.StartAsync(cancellationToken);
            await connection.InvokeCoreAsync("SendAssignmentFailed", new object[]
            {
                assignmentId.ToString(),
                driverId.ToString(),
                dispatcherId.ToString()
            }, cancellationToken);
            await connection.StopAsync(cancellationToken);
        }

        public async Task SendAssignmentExpired(Guid assignmentId, Guid dispatcherId, CancellationToken cancellationToken = default)
        {
            await using HubConnection connection = new HubConnectionBuilder()
                .WithUrl(_configuration.Transport)
                .Build();
            await connection.StartAsync(cancellationToken);
            await connection.InvokeCoreAsync("SendAssignmentExpired", new object[]
            {
                assignmentId.ToString(),
                dispatcherId.ToString()
            }, cancellationToken);
            await connection.StopAsync(cancellationToken);
        }
    }
}
