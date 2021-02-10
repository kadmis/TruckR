using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Events
{
    public class UserRegistered
    {
        public Guid UserId { get; }

        public UserRegistered(Guid id)
        {
            UserId = id;
        }
    }
}
