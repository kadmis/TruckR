using BuildingBlocks.Domain;
using System.Linq;
using Transport.Domain.Groups;

namespace Transport.Domain.TransportGroups.Rules
{
    public class GroupCannotHaveMoreThanGivenNumberOfDrivers : IBusinessRule
    {
        private readonly int _limit;
        private readonly TransportGroup _group;

        public GroupCannotHaveMoreThanGivenNumberOfDrivers(TransportGroup group, int limit)
        {
            _limit = limit;
            _group = group;
        }

        public string Message => $"Group cannot have more than {_limit} drivers";

        public bool IsBroken()
        {
            return _group.Drivers.Count() >= _limit;
        }
    }
}
