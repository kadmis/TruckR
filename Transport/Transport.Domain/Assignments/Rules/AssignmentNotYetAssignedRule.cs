using BuildingBlocks.Domain;

namespace Transport.Domain.Assignments.Rules
{
    public class AssignmentNotYetAssignedRule : IBusinessRule
    {
        private readonly Assignment _assignment;

        public AssignmentNotYetAssignedRule(Assignment assignment)
        {
            _assignment = assignment;
        }

        public string Message => "Assignment not yet assigned";

        public bool IsBroken()
        {
            return !_assignment.Assigned;
        }
    }
}
