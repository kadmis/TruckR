using Auth.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.JWT
{
    public interface ITokenGenerator
    {
        public string GenerateFor(User user);
    }
}
