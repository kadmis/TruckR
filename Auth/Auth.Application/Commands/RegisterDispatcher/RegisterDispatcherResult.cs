using BuildingBlocks.Application.Models.Results;
using System;

namespace Auth.Application.Commands.RegisterDispatcher
{
    public class RegisterDispatcherResult : IResult
    {
        public Guid? Id { get; }
        public string Message { get; }
        public bool Successful { get; }

        private RegisterDispatcherResult(Guid? id, string message, bool successful)
        {
            Id = id;
            Message = message;
            Successful = successful;
        }

        public static RegisterDispatcherResult Success(Guid id)
        {
            return new RegisterDispatcherResult(id, string.Empty, true);
        }

        public static RegisterDispatcherResult Fail(string message)
        {
            return new RegisterDispatcherResult(null, $"Registration failed: {message}", false);
        }
    }
}
