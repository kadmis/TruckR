using BuildingBlocks.Application.Commands;
using System;

namespace Location.Application.Commands.SaveLocation
{
    public class SaveLocationCommand : ICommand
    {
        public Guid UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
