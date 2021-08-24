using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Database;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Database.EFCore;
using Email.API.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Email.API.Infrastructure.Database.Context
{
    public class EmailContext : DbContext, IEventContext
    {
        public DbSet<EmailQueueItem> EmailQueue { get; set; }
        public DbSet<EventEntity> Events { get; set; }

        public EmailContext()
        {
        }
        public EmailContext(DbContextOptions<EmailContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(EventBusAssembly.Assembly);
        }
    }
}
