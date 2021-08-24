using BuildingBlocks.EventBus.Externals.EventualConsistency;
using System;

namespace Auth.IntegrationEvents
{
    public class UserDeletedEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        public UserDeletedEvent(Guid userId, string email) : base()
        {
            UserId = userId;
            Email = email;
        }
    }
}
