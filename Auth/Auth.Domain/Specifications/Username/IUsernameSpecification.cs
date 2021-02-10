using Auth.Domain.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.Username
{
    public interface IUsernameSpecification
    {
        public Task<bool> IsSatisfiedBy(UserName username, CancellationToken cancellationToken = default);
    }
}
