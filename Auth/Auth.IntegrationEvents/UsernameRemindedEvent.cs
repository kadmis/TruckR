using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.IntegrationEvents
{
    public class UsernameRemindedEvent : IntegrationEvent
    {
        public UsernameRemindedEvent(string email, string username)
        {
            Email = email;
            Username = username;
        }

        public string Email { get; }
        public string Username { get; }
    }
}
