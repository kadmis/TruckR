using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Assignments.Rules
{
    public class OneActiveAssignmentPerDriverRule : IBusinessRule
    {
        private readonly Guid _driverId;
        private readonly IDriversActiveAssignment _activeAssignment;

        public OneActiveAssignmentPerDriverRule(
            Guid driverId, 
            IDriversActiveAssignment activeAssignment)
        {
            _driverId = driverId;
            _activeAssignment = activeAssignment;
        }

        public string Message => "Driver can only have one active assignment";

        public bool IsBroken()
        {
            return _activeAssignment.Get(_driverId).HasValue;
        }
    }
}
