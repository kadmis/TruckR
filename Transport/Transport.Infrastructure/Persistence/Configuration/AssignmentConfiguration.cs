using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transport.Domain.Assignments;
using Transport.Domain.Documents;

namespace Transport.Infrastructure.Persistence.Configuration
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property("_title")
                .HasColumnName("Title")
                .IsRequired();
            builder
                .Property("_description")
                .HasColumnName("Description")
                .IsRequired();
            builder
                .OwnsOne<Document>("_transportDocument", x =>
                {
                    x.ToTable("Documents");
                    x.HasKey(x => x.Id);
                    x.WithOwner().HasForeignKey(x => x.AssignmentId);
                    x.Property("_name").HasColumnName("Name");
                    x.Property("_number").HasColumnName("Number");
                });
            builder
                .Property("_dispatcherId")
                .HasColumnName("DispatcherId")
                .IsRequired();
            builder
                .OwnsOne<Address>("_start", x =>
                {
                    x.Property(x => x.Country).HasColumnName("StartingCountry");
                    x.Property(x => x.City).HasColumnName("StartingCity");
                    x.Property(x => x.Street).HasColumnName("StartingStreet");
                    x.Property(x => x.PostalCode).HasColumnName("StartingPostalCode");
                });
            builder
                .OwnsOne<Address>("_destination", x =>
                {
                    x.Property(x => x.Country).HasColumnName("DestinationCountry");
                    x.Property(x => x.City).HasColumnName("DestinationCity");
                    x.Property(x => x.Street).HasColumnName("DestinationStreet");
                    x.Property(x => x.PostalCode).HasColumnName("DestinationPostalCode");
                });
            builder
                .Property("_deadline")
                .HasColumnName("Deadline")
                .IsRequired();
            builder
                .Property("_createdOn")
                .HasColumnName("CreatedOn")
                .IsRequired();
            builder
                .Property("_driverId")
                .HasColumnName("DriverId")
                .IsRequired(false);
            builder
                .Property("_assignedOn")
                .HasColumnName("AssignedOn")
                .IsRequired(false);
            builder
                .Property("_completedOn")
                .HasColumnName("CompletedOn")
                .IsRequired(false);
            builder
                .Property("_failedOn")
                .HasColumnName("FailedOn")
                .IsRequired(false);
        }
    }
}
