using BuildingBlocks.Application.Models.Results;

namespace Transport.Application.Assignments.Commands.TakeAssignment
{
    public class TakeAssignmentResult : IResult
    {
        public string Message { get; private set; }
        public bool Successful { get; private set; }

        private TakeAssignmentResult()
        {

        }

        public static TakeAssignmentResult Success()
        {
            return new TakeAssignmentResult
            {
                Message = string.Empty,
                Successful = true
            };
        }

        public static TakeAssignmentResult Fail(string message)
        {
            return new TakeAssignmentResult
            {
                Message = message,
                Successful = false
            };
        }
    }
}