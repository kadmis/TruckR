using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;

namespace Transport.Application.Assignments.Commands.TakeAssignment
{
    public class TakeAssignmentCommandHandler : ICommandHandler<TakeAssignmentCommand, TakeAssignmentResult>
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IAssignmentsRepository _assignmentsRepository;

        public TakeAssignmentCommandHandler(
            IIdentityAccessor identityAccessor, 
            IAssignmentsRepository assignmentsRepository)
        {
            _identityAccessor = identityAccessor;
            _assignmentsRepository = assignmentsRepository;
        }

        public async Task<TakeAssignmentResult> Handle(TakeAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityAccessor.UserIdentity();

                var assignment = await _assignmentsRepository.Find(request.AssignmentId, cancellationToken);

                assignment.AssignDriver(user.UserId);

                return TakeAssignmentResult.Success();
            }
            catch(Exception ex)
            {
                return TakeAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
