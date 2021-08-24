using BuildingBlocks.Application.Models.Results;

namespace Auth.Application.Commands.Delete
{
    public class DeleteUserResult : IResult
    {
        public string Message { get; }
        public bool Successful { get; }

        private DeleteUserResult(string message, bool successful)
        {
            Message = message;
            Successful = successful;
        }

        public static DeleteUserResult Success()
        {
            return new DeleteUserResult(string.Empty, true);
        }

        public static DeleteUserResult Fail(string message)
        {
            return new DeleteUserResult($"Deletion failed: {message}", false);
        }
    }
}
