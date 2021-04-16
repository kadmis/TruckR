using BuildingBlocks.Application.Models.Results;

namespace Auth.Application.Models.Results
{
    public class UserActivationResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private UserActivationResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static UserActivationResult Success()
        {
            return new UserActivationResult("Successfully activated user.", true);
        }

        public static UserActivationResult Fail(string message)
        {
            return new UserActivationResult($"User activation failed: {message}", false);
        }
    }
}
