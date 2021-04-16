using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Domain.Employees
{
    public interface IEmployeesRepository
    {
        void Add(Employee employee);
    }
}
