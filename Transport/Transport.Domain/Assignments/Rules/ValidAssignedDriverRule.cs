using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Assignments.Rules
{
    public class ValidAssignedDriverRule : IBusinessRule
    {
        private readonly Guid _assignedDriverId;
        private readonly Guid _givenDriverId;

        public ValidAssignedDriverRule(Guid assignedDriverId, Guid givenDriverId)
        {
            _assignedDriverId = assignedDriverId;
            _givenDriverId = givenDriverId;
        }

        public string Message => "Invalid driver for assignment.";

        public bool IsBroken()
        {
            return !_assignedDriverId.Equals(_givenDriverId);
        }
    }
}
