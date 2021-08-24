using BuildingBlocks.Application.Models.Results;
using System;

namespace Auth.Application.Commands.RefreshToken
{
    public class RefreshTokenResult : IResult
    {
        public string Token { get; }
        public Guid? RefreshToken { get; }
        public string Message { get; }
        public bool Successful { get; }

        private RefreshTokenResult(string token, Guid? refreshToken, string message, bool successful)
        {
            Token = token;
            RefreshToken = refreshToken;
            Message = message;
            Successful = successful;
        }

        public static RefreshTokenResult Success(string token, Guid refreshToken)
        {
            return new RefreshTokenResult(token, refreshToken, string.Empty, true);
        }
        public static RefreshTokenResult Fail(string message)
        {
            return new RefreshTokenResult(string.Empty, null, $"Token refresh failed: {message}", false);
        }
    }
}
