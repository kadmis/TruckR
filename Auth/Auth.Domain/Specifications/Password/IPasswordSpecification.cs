using Auth.Domain.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Specifications.Password
{
    public interface IPasswordSpecification
    {
        public bool IsSatisfiedBy(UserPassword password);
    }
}
