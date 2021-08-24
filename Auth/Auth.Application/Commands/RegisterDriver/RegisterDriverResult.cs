using BuildingBlocks.Application.Models.Results;
using System;

namespace Auth.Application.Commands.RegisterDriver
{
    public class RegisterDriverResult : IResult
    {
        public Guid? Id { get; }
        public string Message { get; }
        public bool Successful { get; }

        private RegisterDriverResult(Guid? id, string message, bool successful)
        {
            Id = id;
            Message = message;
            Successful = successful;
        }

        public static RegisterDriverResult Success(Guid id)
        {
            return new RegisterDriverResult(id, string.Empty, true);
        }

        public static RegisterDriverResult Fail(string message)
        {
            return new RegisterDriverResult(null, $"Registration failed: {message}", false);
        }
    }
}
