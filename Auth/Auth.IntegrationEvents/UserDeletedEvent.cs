using Auth.Domain.Data.ValueObjects;
using System;

namespace Auth.IntegrationEvents
{
    public class UserDeletedEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public Email Email { get; }

        public UserDeletedEvent(Guid userId, Email email) : base()
        {
            UserId = userId;
            Email = email;
        }
    }
}
