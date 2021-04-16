using System;

namespace Assignments.API.Infrastructure.Models.Requests
{
    public class CreateAssignmentRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid DocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Deadline { get; set; }
    }
}
