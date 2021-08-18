using BuildingBlocks.Domain;

namespace Transport.Domain.Assignments.Rules
{
    public class AssignmentAlreadyCompletedRule : IBusinessRule
    {
        private readonly Assignment _assignment;

        public AssignmentAlreadyCompletedRule(Assignment assignment)
        {
            _assignment = assignment;
        }

        public string Message => "Assignment is already completed.";

        public bool IsBroken()
        {
            return _assignment.Completed;
        }
    }
}
