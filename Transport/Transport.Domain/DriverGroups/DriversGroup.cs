using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Domain.DriverGroups
{
    public class DriversGroup : IEntity<Guid>, IAggregateRoot
    {
        public Guid Id { get; private set; }

        private string _name;

        private Guid _dispatcherId;

        private DriversGroup()
        {

        }

        public static DriversGroup Create(string name, Guid dispatcherId)
        {
            return new DriversGroup
            {
                _name = name,
                _dispatcherId = dispatcherId
            };
        }
    }
}
