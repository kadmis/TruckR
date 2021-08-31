using BuildingBlocks.Application.Queries;
using System;

namespace Transport.Application.Assignments.Queries.DriversCurrentAssignment
{
    public class DriversCurrentAssignmentQuery : IQuery<DriversCurrentAssignmentResult>
    {
        public Guid DriverId { get; }

        public DriversCurrentAssignmentQuery(Guid driverId)
        {
            DriverId = driverId;
        }
    }
}
