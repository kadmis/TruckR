namespace Auth.API.Infrastructure.Models.CommandsResults
{
    public class SetPasswordResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private SetPasswordResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static SetPasswordResult Success()
        {
            return new SetPasswordResult("Successfully changed password", true);
        }

        public static SetPasswordResult Fail(string message)
        {
            return new SetPasswordResult($"Password change failed: {message}", false);
        }
    }
}
