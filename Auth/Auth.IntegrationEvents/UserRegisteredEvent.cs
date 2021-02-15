using System;

namespace Auth.IntegrationEvents
{
    public class UserRegisteredEvent : IntegrationEvent
    {
        public Guid Id { get; }
        public Guid ActivationId { get; }
        public string Email { get; }

        public UserRegisteredEvent(Guid id, Guid activationId, string email)
        {
            Id = id;
            ActivationId = activationId;
            OccuredOn = DateTime.Now;
            Email = email;
        }
    }
}
