using BuildingBlocks.Domain;

namespace Location.Domain.ValueObjects
{
    public class GeodeticCurve : ValueObject
    {
        public double EllipsoidalDistance { get; }
        public Angle Azimuth { get; }
        public Angle ReverseAzimuth { get; }

        private GeodeticCurve(double ellipsoidalDistance, Angle azimuth, Angle reverseAzimuth)
        {
            EllipsoidalDistance = ellipsoidalDistance;
            Azimuth = azimuth;
            ReverseAzimuth = reverseAzimuth;
        }

        public static GeodeticCurve Create(double ellipsoidalDistance, Angle azimuth, Angle reverseAzimuth)
        {
            return new GeodeticCurve(ellipsoidalDistance, azimuth, reverseAzimuth);
        }
    }
}
