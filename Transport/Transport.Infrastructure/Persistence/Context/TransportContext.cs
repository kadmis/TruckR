using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Transport.Domain.Assignments;
using Transport.Domain.Dispatchers;
using Transport.Domain.Documents;
using Transport.Domain.DriverGroups;
using Transport.Domain.Drivers;

namespace Transport.Infrastructure.Persistence.Context
{
    public class TransportContext : DbContext
    {
        public DbSet<Assignment> Assignments { get; }
        public DbSet<Dispatcher> Dispatchers { get; }
        public DbSet<Document> Documents { get; }
        //public DbSet<DriversGroup> DriversGroups { get; }
        public DbSet<Driver> Drivers { get; }

        public TransportContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
