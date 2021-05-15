using BuildingBlocks.Application.Commands;
using Microsoft.AspNetCore.Http;
using System;

namespace Transport.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentCommand : ICommand<CreateAssignmentResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Document { get; set; }

        public string StartingCountry { get; set; }
        public string StartingCity { get; set; }
        public string StartingStreet { get; set; }
        public string StartingPostalCode { get; set; }

        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationPostalCode { get; set; }

        public DateTime Deadline { get; set; }
    }
}
