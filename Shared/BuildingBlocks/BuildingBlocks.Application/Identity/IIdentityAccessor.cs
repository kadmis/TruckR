using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Application.Identity
{
    public interface IIdentityAccessor
    {
        public Identity UserIdentity();
    }
}
