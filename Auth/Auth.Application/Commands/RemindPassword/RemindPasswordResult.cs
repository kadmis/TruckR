using BuildingBlocks.Application.Models.Results;

namespace Auth.Application.Commands.RemindPassword
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
            return new RemindPasswordResult(string.Empty, true);
        }

        public static RemindPasswordResult Fail(string message)
        {
            return new RemindPasswordResult($"Remind password failed: {message}", false);
        }
    }
}
