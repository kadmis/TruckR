using System;

namespace Location.Application.DTO
{
    public class LocationModel
    {
        public Guid UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationModel(Guid userId, double latitude, double longitude)
        {
            UserId = userId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
