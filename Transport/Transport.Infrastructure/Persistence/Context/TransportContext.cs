using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Database;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Database.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Transport.Domain.Assignments;
using Transport.Domain.Groups;

namespace Transport.Infrastructure.Persistence.Context
{
    public class TransportContext : DbContext, IEventContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<TransportGroup> TransportGroups { get; set; }
        public DbSet<EventEntity> Events { get; set; }

        public TransportContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(EventBusAssembly.Assembly);
        }
    }
}
