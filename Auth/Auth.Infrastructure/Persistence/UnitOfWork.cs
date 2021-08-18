using Auth.Domain.Persistence;
using Auth.Domain.Persistence.Repositories;
using Auth.Infrastructure.Persistence.Context;
using Auth.Infrastructure.Persistence.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthContext _context;
        private IUserRepository _userRepository;
        private IUserAuthenticationRepository _userAuthenticationRepository;

        public UnitOfWork(AuthContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IUserAuthenticationRepository UserAuthenticationRepository => _userAuthenticationRepository ??= new UserAuthenticationRepository(_context);

        public Task<int> Save(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
