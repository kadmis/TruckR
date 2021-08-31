using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;

namespace Transport.Application.Assignments.Commands.TakeAssignment
{
    public class TakeAssignmentCommandHandler : ICommandHandler<TakeAssignmentCommand, TakeAssignmentResult>
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly ISqlConnectionFactory _connectionFactory;

        public TakeAssignmentCommandHandler(
            IIdentityAccessor identityAccessor,
            IAssignmentsRepository assignmentsRepository, 
            ISqlConnectionFactory connectionFactory)
        {
            _identityAccessor = identityAccessor;
            _assignmentsRepository = assignmentsRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<TakeAssignmentResult> Handle(TakeAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityAccessor.UserIdentity();

                var assignment = await _assignmentsRepository.Find(request.AssignmentId, cancellationToken);

                assignment.AssignDriver(user.UserId, new DriversActiveAssignment(_connectionFactory));

                return TakeAssignmentResult.Success();
            }
            catch(Exception ex)
            {
                return TakeAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
