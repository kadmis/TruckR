namespace Auth.API.Infrastructure.Models.CommandsResults
{
    public class RemindPasswordResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private RemindPasswordResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static RemindPasswordResult Success()
        {
            return new RemindPasswordResult("Please check your mailbox.", true);
        }

        public static RemindPasswordResult Fail(string message)
        {
            return new RemindPasswordResult($"Remind password failed: {message}", false);
        }
    }
}
