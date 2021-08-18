using Email.API.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Email.API.Infrastructure.Database.Configuration
{
    public class EmailQueueItemConfiguration : IEntityTypeConfiguration<EmailQueueItem>
    {
        public void Configure(EntityTypeBuilder<EmailQueueItem> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
