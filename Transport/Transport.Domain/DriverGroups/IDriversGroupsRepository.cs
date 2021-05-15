using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Domain.DriverGroups
{
    public interface IDriversGroupsRepository
    {
        DriversGroup Add(DriversGroup driversGroup);
    }
}
