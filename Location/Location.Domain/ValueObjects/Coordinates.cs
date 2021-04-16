using BuildingBlocks.Domain;

namespace Location.Domain.ValueObjects
{
    public class Coordinates : ValueObject
    {
        public Angle Latitude { get; private set; }
        public Angle Longitude { get; private set; }

        private Coordinates(Angle latitude, Angle longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Coordinates Create(double latitude, double longitude)
        {
            return new Coordinates(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude));
        }
    }
}
