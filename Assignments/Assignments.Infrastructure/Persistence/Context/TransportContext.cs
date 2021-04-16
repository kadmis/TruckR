using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Transport.Domain.Assignments;
using Transport.Domain.Employees;

namespace Transport.Infrastructure.Persistence.Context
{
    public class TransportContext : DbContext
    {
        public DbSet<Assignment> Assignments { get; }
        public DbSet<Employee> Actors { get; }

        public TransportContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
