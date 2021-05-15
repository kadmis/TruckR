using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Domain.Dispatchers;

namespace Transport.Infrastructure.Persistence.Configuration
{
    public class DispatchersConfiguration : IEntityTypeConfiguration<Dispatcher>
    {
        public void Configure(EntityTypeBuilder<Dispatcher> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property("_fullname")
                .HasColumnName("Fullname");

            builder
                .Property("_email")
                .HasColumnName("Email");

            builder
                .Property("_phoneNumber")
                .HasColumnName("Phone");
        }
    }
}
