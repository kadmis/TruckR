using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.UsernameSpecifications.Interfaces
{
    public interface IUsernameExistsOnOtherUsers : IUsernameSpecification
    {
        IUsernameExistsOnOtherUsers Setup(Guid currentUserId);
    }
}
