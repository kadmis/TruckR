using BuildingBlocks.Application.Models.Results;

namespace Transport.Application.Assignments.Commands.FinishAssignment
{
    public class FinishAssignmentResult : IResult
    {
        public string Message { get; private set; }
        public bool Successful { get; private set; }

        private FinishAssignmentResult()
        {
        }

        public static FinishAssignmentResult Success()
        {
            return new FinishAssignmentResult
            {
                Message = string.Empty,
                Successful = true
            };
        }

        public static FinishAssignmentResult Fail(string message)
        {
            return new FinishAssignmentResult
            {
                Message = message,
                Successful = false
            };
        }
    }
}