using BuildingBlocks.Application.Models.Results;
using System;

namespace Transport.Application.Assignments.Queries.DriversCurrentAssignment
{
    public class DriversCurrentAssignmentResult : IResult<AssignmentDetailsDTO>
    {
        private DriversCurrentAssignmentResult()
        {
        }

        public string Message { get; private set; }
        public bool Successful { get; private set; }
        public AssignmentDetailsDTO Data { get; private set; }

        public static DriversCurrentAssignmentResult Success(AssignmentDetailsDTO details)
        {
            return new DriversCurrentAssignmentResult
            {
                Successful = true,
                Message = string.Empty,
                Data = details
            };
        }

        public static DriversCurrentAssignmentResult Fail(string message)
        {
            return new DriversCurrentAssignmentResult
            {
                Successful = false,
                Message = message,
            };
        }
    }

    public class AssignmentDetailsDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Dispatcher { get; set; }

        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostalCode { get; set; }

        public string StartingCountry { get; set; }
        public string StartingCity { get; set; }
        public string StartingStreet { get; set; }
        public string StartingPostalCode { get; set; }

        public DateTime Deadline { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public DateTime? AssignedOn { get; set; }
    }
}
