using Auth.Domain.Data.Entities;
using Auth.Domain.Security.Encryption;
using Auth.Domain.Security.Passwords;
using Auth.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Context
{
    public class AuthContext : DbContext
    {
        private readonly IEncryptor _encryptor;
        private readonly IPasswordHasher _hasher;

        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthentication> UserAuthentications { get; set; }
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }
        public AuthContext(DbContextOptions<AuthContext> options, IEncryptor encryptor, IPasswordHasher hasher) : base(options)
        {
            _encryptor = encryptor;
            _hasher = hasher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration(_hasher, _encryptor));
            modelBuilder.ApplyConfiguration(new UserAuthenticationConfiguration());
        }
    }
}
