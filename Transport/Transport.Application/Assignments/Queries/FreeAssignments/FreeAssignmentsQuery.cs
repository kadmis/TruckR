using BuildingBlocks.Application.Queries;

namespace Transport.Application.Assignments.Queries.FreeAssignments
{
    public class FreeAssignmentsQuery : IQuery<FreeAssignmentsResult>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
