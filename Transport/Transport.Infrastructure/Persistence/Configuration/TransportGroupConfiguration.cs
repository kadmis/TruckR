using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transport.Domain.Groups;

namespace Transport.Infrastructure.Persistence.Configuration
{
    public class TransportGroupConfiguration : IEntityTypeConfiguration<TransportGroup>
    {
        public void Configure(EntityTypeBuilder<TransportGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsMany<Driver>("Drivers", x =>
            {
                x.ToTable("Drivers");
                x.HasKey(x => new { x.Id, x.GroupId });
                x.WithOwner().HasForeignKey(x=>x.GroupId);
            });

            builder
                .Property("_dispatcherId")
                .HasColumnName("DispatcherId");

            builder
                .Metadata
                .FindNavigation("Drivers")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
