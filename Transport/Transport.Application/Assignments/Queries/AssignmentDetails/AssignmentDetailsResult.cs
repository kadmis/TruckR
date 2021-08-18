using BuildingBlocks.Application.Models.Results;
using System;

namespace Transport.Application.Assignments.Queries.AssignmentDetails
{
    public class AssignmentDetailsResult : IResult
    {
        public AssignmentDetailsDTO Data { get; private set; }
        public string Message { get; private set; }
        public bool Successful { get; private set; }

        private AssignmentDetailsResult()
        {
        }

        public static AssignmentDetailsResult Success(AssignmentDetailsDTO data)
        {
            return new AssignmentDetailsResult
            {
                Data = data,
                Message = string.Empty,
                Successful = true
            };
        }

        public static AssignmentDetailsResult Fail(string message)
        {
            return new AssignmentDetailsResult
            {
                Message = message,
                Successful = false
            };
        }
    }

    public class AssignmentDetailsDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Dispatcher { get; set; }
        public string Driver { get; set; }

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