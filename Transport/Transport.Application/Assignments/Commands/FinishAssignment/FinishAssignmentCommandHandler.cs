using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;

namespace Transport.Application.Assignments.Commands.FinishAssignment
{
    public class FinishAssignmentCommandHandler : ICommandHandler<FinishAssignmentCommand, FinishAssignmentResult>
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IAssignmentsRepository _assignmentsRepository;

        public FinishAssignmentCommandHandler(
            IIdentityAccessor identityAccessor, 
            IAssignmentsRepository assignmentsRepository)
        {
            _identityAccessor = identityAccessor;
            _assignmentsRepository = assignmentsRepository;
        }

        public async Task<FinishAssignmentResult> Handle(FinishAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityAccessor.UserIdentity();

                var assignment = await _assignmentsRepository.Find(request.AssignmentId, cancellationToken);

                //assignment.Complete(driver);

                return FinishAssignmentResult.Success();
            }
            catch(Exception ex)
            {
                return FinishAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
