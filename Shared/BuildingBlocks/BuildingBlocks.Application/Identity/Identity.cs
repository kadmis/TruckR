using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Application.Identity
{

    public class Identity
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Role { get; private set; }

        private Identity()
        {

        }

        public static Identity CreateIdentity(Guid userId, string name, string role)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("Invalid user id");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid user name");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Invalid role");
            }

            return new Identity
            {
                UserId = userId,
                Name = name,
                Role = role
            };
        }
    }
}
