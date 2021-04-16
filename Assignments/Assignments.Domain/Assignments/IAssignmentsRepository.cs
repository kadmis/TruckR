using System.Threading.Tasks;

namespace Transport.Domain.Assignments
{
    public interface IAssignmentsRepository
    {
        void Add(Assignment assignment);
    }
}
