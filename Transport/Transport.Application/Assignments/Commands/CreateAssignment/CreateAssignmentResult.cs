using BuildingBlocks.Application.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentResult : IResult
    {
        public string Message { get; private set; }
        public bool Successful { get; private set; }
        public Guid? AssignmentId { get; private set; }

        private CreateAssignmentResult()
        {
        }

        public static CreateAssignmentResult Success(Guid assignmentId)
        {
            return new CreateAssignmentResult
            {
                Message = string.Empty,
                Successful = true,
                AssignmentId = assignmentId
            };
        }

        public static CreateAssignmentResult Fail(string message)
        {
            return new CreateAssignmentResult
            {
                Message = message,
                Successful = false
            };
        }
    }
}
