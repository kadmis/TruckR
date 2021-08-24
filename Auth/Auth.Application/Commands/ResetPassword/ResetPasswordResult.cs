using BuildingBlocks.Application.Models.Results;

namespace Auth.Application.Commands.ResetPassword
{
    public class ResetPasswordResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private ResetPasswordResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static ResetPasswordResult Success()
        {
            return new ResetPasswordResult(string.Empty, true);
        }

        public static ResetPasswordResult Fail(string message)
        {
            return new ResetPasswordResult($"Password reset failed: {message}", false);
        }
    }
}
