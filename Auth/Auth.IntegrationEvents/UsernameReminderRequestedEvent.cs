using BuildingBlocks.EventBus.EventualConsistency;

namespace Auth.IntegrationEvents
{
    public class UsernameReminderRequestedEvent : IntegrationEvent
    {
        public UsernameReminderRequestedEvent(string email, string username) : base()
        {
            Email = email;
            Username = username;
        }
        public UsernameReminderRequestedEvent()
        {

        }
        public string Email { get; }
        public string Username { get; }
    }
}
