using Auth.Domain.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.IntegrationEvents
{
    public class DispatcherActivatedEvent : UserActivatedEvent
    {
        public DispatcherActivatedEvent(
            Guid id, 
            string email, 
            string firstname, 
            string lastname, 
            string phoneNumber) : base(id, email, firstname, lastname, phoneNumber)
        {
        }
    }
}
