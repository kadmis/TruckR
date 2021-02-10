using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence.Repositories;
using Auth.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _context;

        public UserRepository(AuthContext context)
        {
            _context = context;
        }

        public Guid Add(User user)
        {
            _context.Users.Add(user);
            return user.Id;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            user.Delete();
        }

        public Task<bool> UsernameExists(UserName username, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(x => x.Username.Username.Equals(username.Username), cancellationToken);
        }

        public Task<bool> EmailExists(UserEmail email, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(x => x.Email.Email.Equals(email.Email), cancellationToken);
        }

        public Task<User> FindByUsername(UserName username, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Username.Username.Equals(username.Username), cancellationToken);
        }

        public Task<User> FindById(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<User> FindByEmail(UserEmail email, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email.Email.Equals(email.Email), cancellationToken);
        }

        public async Task<bool> UsernameExistsOnOtherUsers(UserName username, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(x => x.Username.Username.Equals(username.Username) && !x.Id.Equals(currentUserId));
        }

        public async Task<bool> EmailExistsOnOtherUsers(UserEmail email, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(x => x.Email.Email.Equals(email.Email) && !x.Id.Equals(currentUserId));
        }
    }
}
