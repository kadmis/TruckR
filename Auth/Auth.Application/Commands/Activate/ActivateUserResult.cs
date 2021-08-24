using BuildingBlocks.Application.Models.Results;

namespace Auth.Application.Commands.Activate
{
    public class ActivateUserResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private ActivateUserResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static ActivateUserResult Success()
        {
            return new ActivateUserResult(string.Empty, true);
        }

        public static ActivateUserResult Fail(string message)
        {
            return new ActivateUserResult($"User activation failed: {message}", false);
        }
    }
}
