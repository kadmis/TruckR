using Auth.Domain.Data.Entities;
using Auth.Domain.Security.Encryption;
using Auth.Domain.Security.Passwords;
using Auth.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Context
{
    public class AuthContext : DbContext
    {
        private readonly IEncryptor encryptor;
        private readonly IPasswordHasher hasher;

        public DbSet<User> Users { get; set; }
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }
        public AuthContext(DbContextOptions<AuthContext> options, IEncryptor encryptor, IPasswordHasher hasher) : base(options)
        {
            this.encryptor = encryptor;
            this.hasher = hasher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration(hasher, encryptor));
        }
    }
}
