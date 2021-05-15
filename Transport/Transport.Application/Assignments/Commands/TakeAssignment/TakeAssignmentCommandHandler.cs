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

namespace Transport.Application.Assignments.Commands.TakeAssignment
{
    public class TakeAssignmentCommandHandler : ICommandHandler<TakeAssignmentCommand, TakeAssignmentResult>
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly IDriversRepository _driversRepository;

        public TakeAssignmentCommandHandler(
            IIdentityAccessor identityAccessor, 
            IAssignmentsRepository assignmentsRepository, 
            IDriversRepository driversRepository)
        {
            _identityAccessor = identityAccessor;
            _assignmentsRepository = assignmentsRepository;
            _driversRepository = driversRepository;
        }

        public async Task<TakeAssignmentResult> Handle(TakeAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityAccessor.UserIdentity();

                var assignment = await _assignmentsRepository.Find(request.AssignmentId, cancellationToken);

                var driver = await _driversRepository.FindById(user.UserId, cancellationToken);

                assignment.AssignDriver(driver);

                return TakeAssignmentResult.Success();
            }
            catch(Exception ex)
            {
                return TakeAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
