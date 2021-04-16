using BuildingBlocks.Application.Models.Results;
using System;

namespace Auth.Application.Models.Results
{
    public class RegistrationResult : IResult
    {
        public Guid? Id { get; }
        public string Message { get; }
        public bool Successful { get; }

        private RegistrationResult(Guid? id, string message, bool successful)
        {
            Id = id;
            Message = message;
            Successful = successful;
        }

        public static RegistrationResult Success(Guid id)
        {
            return new RegistrationResult(id, "Registration successful. Please check your inbox for activation link.", true);
        }

        public static RegistrationResult Fail(string message)
        {
            return new RegistrationResult(null, $"Registration failed: {message}", false);
        }
    }
}
