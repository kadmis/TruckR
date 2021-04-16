using Auth.Domain.Data.ValueObjects;
using System;

namespace Auth.IntegrationEvents
{
    public class UserRegisteredEvent : IntegrationEvent
    {
        public Guid Id { get; }
        public Guid ActivationId { get; }
        public Email Email { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public string Role { get; }

        public UserRegisteredEvent(Guid id, Guid activationId, Email email, string firstname, string lastname, string role) : base()
        {
            Id = id;
            ActivationId = activationId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Role = role;
        }
    }
}
