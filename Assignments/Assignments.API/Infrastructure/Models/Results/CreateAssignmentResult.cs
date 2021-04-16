using BuildingBlocks.API.Infrastructure.Models.Results;

namespace Assignments.API.Infrastructure.Models.Results
{
    public class CreateAssignmentResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private CreateAssignmentResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static CreateAssignmentResult Success()
        {
            return new CreateAssignmentResult("Successfully created an assignment. It awaits for being taken.", true);
        }

        public static CreateAssignmentResult Fail(string message)
        {
            return new CreateAssignmentResult($"Couldn;t create new assignment: {message}", false);
        }
    }
}
