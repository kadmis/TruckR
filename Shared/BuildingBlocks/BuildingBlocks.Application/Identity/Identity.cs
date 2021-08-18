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
        public Guid AuthenticationId { get; private set; }

        private Identity()
        {

        }

        public static Identity CreateIdentity(Guid userId, string name, string role, Guid authenticationId)
        {
            return new Identity
            {
                UserId = userId,
                Name = name,
                Role = role,
                AuthenticationId = authenticationId
            };
        }
    }
}
