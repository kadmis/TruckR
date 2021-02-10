using Auth.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Authentication
{
    public interface IUserAuthenticationService
    {
        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<User> Authenticate(string username, string password, CancellationToken cancellationToken = default);
    }
}
