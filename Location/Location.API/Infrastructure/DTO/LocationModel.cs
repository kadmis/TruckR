using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Infrastructure.DTO
{
    public class LocationModel
    {
        public string User { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
