using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
                throw new UserDoesntExistException();
            }

            user.Delete();
        }

        public Task<bool> UsernameExists(Username username, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(x => x.Username.Value.Equals(username.Value), cancellationToken);
        }

        public Task<bool> EmailExists(Email email, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(x => x.Email.Value.Equals(email.Value), cancellationToken);
        }

        public Task<User> FindByUsername(Username username, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Username.Value.Equals(username.Value), cancellationToken);
        }

        public Task<User> FindById(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<User> FindByEmail(Email email, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email.Value.Equals(email.Value), cancellationToken);
        }

        public Task<User> FindByEmailAndUsername(Email email, Username username, CancellationToken cancellationToken = default)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email.Value.Equals(email.Value) && x.Username.Value.Equals(username.Value), cancellationToken);
        }

        public Task<bool> UsernameExistsOnOtherUsers(Username username, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(x => x.Username.Value.Equals(username.Value) && !x.Id.Equals(currentUserId), cancellationToken);
        }

        public Task<bool> EmailExistsOnOtherUsers(Email email, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(x => x.Email.Value.Equals(email.Value) && !x.Id.Equals(currentUserId), cancellationToken);
        }
    }
}
