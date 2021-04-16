using Auth.Domain.Data.ValueObjects;

namespace Auth.IntegrationEvents
{
    public class UsernameReminderRequestedEvent : IntegrationEvent
    {
        public UsernameReminderRequestedEvent(Email email, Username username) : base()
        {
            Email = email;
            Username = username;
        }

        public Email Email { get; }
        public Username Username { get; }
    }
}
