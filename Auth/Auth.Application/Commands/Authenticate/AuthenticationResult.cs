using BuildingBlocks.Application.Models.Results;
using System;

namespace Auth.Application.Commands.Authenticate
{
    public class AuthenticationResult : IResult
    {
        public string Token { get; }
        public Guid? RefreshToken { get; }
        public string Message { get; }
        public bool Successful { get; }

        private AuthenticationResult(string token, Guid? refreshToken, string message, bool successful)
        {
            Token = token;
            RefreshToken = refreshToken;
            Message = message;
            Successful = successful;
        }

        public static AuthenticationResult Success(string token, Guid refreshToken)
        {
            return new AuthenticationResult(token, refreshToken, string.Empty, true);
        }
        public static AuthenticationResult Fail(string message)
        {
            return new AuthenticationResult(string.Empty, null, $"Authentication failed: {message}", false);
        }
    }
}
