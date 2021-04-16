using Location.Domain.ValueObjects;
using ProtoBuf;
using System;

namespace Location.Infrastructure.Cache.Models
{
    [ProtoContract]
    public class CachedLocation
    {
        [ProtoMember(1)]
        public double Latitude { get; set; }

        [ProtoMember(2)]
        public double Longitude { get; set; }

        [ProtoMember(3)]
        public DateTime TimeStamp { get; set; }

        [ProtoMember(4)]
        public Guid UserId { get; set; }

        public CachedLocation(double latitude, double longitude, Guid userId)
        {
            Latitude = latitude;
            Longitude = longitude;
            UserId = userId;
            TimeStamp = DateTime.UtcNow;
        }
        public CachedLocation()
        {

        }

        public static string GenerateKey(DateTime timeStamp)
        {
            var datePart = new DateTime(timeStamp.Year, timeStamp.Month, timeStamp.Day, timeStamp.Hour, 0, 0).ToString("dd-MM-yyyy, HH");
            return $"|Locations|{datePart}|";
        }
        public static string GenerateKey()
        {
            return GenerateKey(DateTime.UtcNow);
        }

        public Coordinates ToCoordinates()
        {
            return Coordinates.Create(Latitude, Longitude);
        }
    }
}
