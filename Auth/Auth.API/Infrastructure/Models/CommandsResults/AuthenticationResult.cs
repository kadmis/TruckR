using Auth.API.Infrastructure.Models.CommandsResults;

namespace Auth.API.Infrastructure.Models
{
    public class AuthenticationResult : IResult
    {
        public string Token { get; }
        public string Message { get; }
        public bool Successful { get; }

        private AuthenticationResult(string token, string message, bool successful)
        {
            Token = token;
            Message = message;
            Successful = successful;
        }

        public static AuthenticationResult Success(string token)
        {
            return new AuthenticationResult(token, $"Authentication successful.", true);
        }
        public static AuthenticationResult Fail(string message)
        {
            return new AuthenticationResult(string.Empty, $"Authentication failed: {message}", false);
        }
    }
}
