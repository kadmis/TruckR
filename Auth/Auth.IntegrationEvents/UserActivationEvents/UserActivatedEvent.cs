using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.IntegrationEvents
{
    public class UserActivatedEvent : IntegrationEvent
    {
        public UserActivatedEvent(Guid id, string email, string firstname, string lastname, string phoneNumber)
        {
            Id = id;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
        }

        public Guid Id { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Firstname { get; }
        public string Lastname { get; }
    }
}
