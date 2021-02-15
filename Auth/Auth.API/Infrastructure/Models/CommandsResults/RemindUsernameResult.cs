using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Models.CommandsResults
{
    public class RemindUsernameResult : IResult
    {
        public string Message { get; }

        public bool Successful { get; }

        private RemindUsernameResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static RemindUsernameResult Success()
        {
            return new RemindUsernameResult("Please check your mailbox.", true);
        }

        public static RemindUsernameResult Fail(string message)
        {
            return new RemindUsernameResult($"Remind username failed: {message}", false);
        }
    }
}
