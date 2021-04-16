using BuildingBlocks.Application.Commands;
using System;

namespace Location.Application.Commands
{
    public class SaveLocationCommand : ICommand
    {
        public Guid UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
