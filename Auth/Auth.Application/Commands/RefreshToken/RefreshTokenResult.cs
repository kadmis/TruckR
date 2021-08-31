using BuildingBlocks.Application.Models.Results;
using System;

namespace Auth.Application.Commands.RefreshToken
{
    public class RefreshTokenResult : IResult
    {
        public string Token { get; }
        public Guid? RefreshToken { get; }
        public long? RefreshInterval { get; }
        public string Message { get; }
        public bool Successful { get; }

        private RefreshTokenResult(string token, Guid? refreshToken, long? refreshInterval, string message, bool successful)
        {
            Token = token;
            RefreshToken = refreshToken;
            Message = message;
            RefreshInterval = refreshInterval;
            Successful = successful;
        }

        public static RefreshTokenResult Success(string token, Guid refreshToken, long refreshInterval)
        {
            return new RefreshTokenResult(token, refreshToken, refreshInterval, string.Empty, true);
        }
        public static RefreshTokenResult Fail(string message)
        {
            return new RefreshTokenResult(string.Empty, null, null, $"Token refresh failed: {message}", false);
        }
    }
}
