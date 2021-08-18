using BuildingBlocks.Domain;

namespace Transport.Domain.Assignments.Rules
{
    public class AssignmentAlreadyAssignedRule : IBusinessRule
    {
        private readonly Assignment _assignment;

        public AssignmentAlreadyAssignedRule(Assignment assignment)
        {
            _assignment = assignment;
        }

        public string Message => "Assignment is already assigned.";

        public bool IsBroken()
        {
            return _assignment.Assigned;
        }
    }
}
