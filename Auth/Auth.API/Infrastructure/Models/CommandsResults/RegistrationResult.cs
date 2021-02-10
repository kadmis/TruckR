using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Models.CommandsResults
{
    public class RegistrationResult
    {
        public Guid? Id { get; }
        public string Message { get; }
        public bool Successful { get; }

        private RegistrationResult(Guid id, string message, bool successful)
        {
            Id = id;
            Message = message;
            Successful = successful;
        }

        public static RegistrationResult Success(Guid id)
        {
            return new RegistrationResult(id, "Registration successful. Welcome to the TruckR system.", true);
        }

        public static RegistrationResult Fail(string message)
        {
            return new RegistrationResult(Guid.Empty, $"Registration failed: {message}", false);
        }
    }
}
