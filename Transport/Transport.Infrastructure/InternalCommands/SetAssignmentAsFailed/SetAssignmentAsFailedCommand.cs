using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Handlers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;

namespace Transport.Infrastructure.InternalCommands.SetAssignmentAsFailed
{
    public class SetAssignmentAsFailedCommand : ICommand<bool>
    {
        public Guid AssignmentId { get; }

        public SetAssignmentAsFailedCommand(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }

    public class SetAssignmentAsFailedCommandHandler : ICommandHandler<SetAssignmentAsFailedCommand, bool>
    {
        private readonly IAssignmentsRepository _repository;

        public SetAssignmentAsFailedCommandHandler(IAssignmentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SetAssignmentAsFailedCommand request, CancellationToken cancellationToken)
        {
            bool success;

            var assignment = await _repository.Find(request.AssignmentId, cancellationToken);

            if (success = assignment != null)
                assignment.Fail();

            return success;
        }
    }
}
