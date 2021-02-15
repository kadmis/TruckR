using System;

namespace Auth.API.Infrastructure.Models.CommandsResults
{
    public class UserDeletionResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private UserDeletionResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static UserDeletionResult Success(Guid id)
        {
            return new UserDeletionResult($"Successfully deleted user ({id})", true);
        }

        public static UserDeletionResult Fail(string message)
        {
            return new UserDeletionResult($"Deletion failed: {message}", false);
        }
    }
}
