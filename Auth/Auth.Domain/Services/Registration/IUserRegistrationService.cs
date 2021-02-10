using Auth.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Registration
{
    public interface IUserRegistrationService
    {
        public Task<User> Register(string username, string password, string email, CancellationToken cancellationToken = default);
    }
}
