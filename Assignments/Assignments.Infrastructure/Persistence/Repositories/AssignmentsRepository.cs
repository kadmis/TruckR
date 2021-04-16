using Transport.Domain.Assignments;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        private readonly TransportContext _assignmentsContext;

        public AssignmentsRepository(TransportContext assignmentsContext)
        {
            _assignmentsContext = assignmentsContext;
        }

        public void Add(Assignment assignment)
        {
            _assignmentsContext.Assignments.Add(assignment);
        }
    }
}
