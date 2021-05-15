using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;
using Transport.Domain.Drivers;

namespace Transport.Application.Assignments.Commands.FinishAssignment
{
    public class FinishAssignmentCommandHandler : ICommandHandler<FinishAssignmentCommand, FinishAssignmentResult>
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly IDriversRepository _driversRepository;

        public FinishAssignmentCommandHandler(IIdentityAccessor identityAccessor, IAssignmentsRepository assignmentsRepository, IDriversRepository driversRepository)
        {
            _identityAccessor = identityAccessor;
            _assignmentsRepository = assignmentsRepository;
            _driversRepository = driversRepository;
        }

        public async Task<FinishAssignmentResult> Handle(FinishAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityAccessor.UserIdentity();

                var assignment = await _assignmentsRepository.Find(request.AssignmentId, cancellationToken);

                var driver = await _driversRepository.FindById(user.UserId, cancellationToken);

                assignment.Complete(driver);

                return FinishAssignmentResult.Success();
            }
            catch(Exception ex)
            {
                return FinishAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
