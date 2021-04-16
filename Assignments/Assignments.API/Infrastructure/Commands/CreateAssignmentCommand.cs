using Assignments.API.Infrastructure.Models.Requests;
using Assignments.API.Infrastructure.Models.Results;
using BuildingBlocks.API.Infrastructure.Commands;
using BuildingBlocks.API.Infrastructure.Helpers;
using System;

namespace Assignments.API.Infrastructure.Commands
{
    public class CreateAssignmentCommand : ICommand<CreateAssignmentResult>
    {
        private CreateAssignmentCommand()
        {

        }
        private CreateAssignmentCommand(string title, string description, Guid documentId, string documentNumber, Guid dispatcherId, string dispatcherName, DateTime deadline)
        {
            Title = title;
            Description = description;
            DocumentId = documentId;
            DocumentNumber = documentNumber;
            DispatcherId = dispatcherId;
            DispatcherName = dispatcherName;
            Deadline = deadline;
        }

        public string Title { get; }
        public string Description { get; }

        public Guid DocumentId { get; }
        public string DocumentNumber { get; }

        public Guid DispatcherId { get; }
        public string DispatcherName { get; }

        public DateTime Deadline { get; }

        public static CreateAssignmentCommand FromRequest(CreateAssignmentRequest request, Identity dispatcherIdentity)
        {
            return new CreateAssignmentCommand(
                request.Title,
                request.Description,
                request.DocumentId,
                request.DocumentNumber,
                dispatcherIdentity.UserId,
                dispatcherIdentity.Name,
                request.Deadline);
        }
    }
}
