using BuildingBlocks.EventBus.EventualConsistency;
using System;

namespace Auth.IntegrationEvents
{
    public class UserRegisteredEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public Guid ActivationId { get; }
        public string Email { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public string Role { get; }

        public UserRegisteredEvent(Guid userId, Guid activationId, string email, string firstname, string lastname, string role) : base()
        {
            UserId = userId;
            ActivationId = activationId;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Role = role;
        }
    }
}
