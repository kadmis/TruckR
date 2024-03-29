﻿using Auth.Domain.Data.Entities;
using Auth.Domain.Security.Encryption;
using Auth.Domain.Security.Passwords;
using Auth.Infrastructure.Security.Encryption;
using Auth.Infrastructure.Security.Passwords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly IPasswordHasher _hasher;
        private readonly IEncryptor _encryptor;

        public UserEntityConfiguration(IPasswordHasher hasher, IEncryptor encryptor)
        {
            _hasher = hasher;
            _encryptor = encryptor;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
            builder
                .OwnsOne(x => x.Username)
                .Property(x => x.Value)
                .HasConversion(new EncryptedConverter(_encryptor))
                .HasColumnName("Username");
            builder
                .OwnsOne(x => x.Email)
                .Property(x => x.Value)
                .HasConversion(new EncryptedConverter(_encryptor))
                .HasColumnName("Email");
            builder
                .OwnsOne(x => x.Password)
                .Property(x => x.Value)
                .HasConversion(new HashedPasswordConverter(_hasher))
                .HasColumnName("Password");
            builder
                .OwnsOne(x => x.PhoneNumber)
                .Property(x => x.Number)
                .HasConversion(new EncryptedConverter(_encryptor))
                .HasColumnName("Phone");
            builder
                .OwnsOne(x => x.Role)
                .Property(x => x.Value)
                .HasColumnName("Role");
            builder
                .Property(x => x.Firstname)
                .HasConversion(new EncryptedConverter(_encryptor))
                .HasColumnName("Firstname");
            builder
                .Property(x => x.Lastname)
                .HasConversion(new EncryptedConverter(_encryptor))
                .HasColumnName("Lastname");
        }
    }
}
