using System;
using Transport.Domain.Employees;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly TransportContext _context;

        public EmployeesRepository(TransportContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Actors.Add(employee);
        }
    }
}
