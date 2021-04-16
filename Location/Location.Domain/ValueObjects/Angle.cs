using BuildingBlocks.Domain;
using System;

namespace Location.Domain.ValueObjects
{
    public class Angle : ValueObject
    {
        private const double PiOver180 = Math.PI / 180.0;
        public static readonly Angle Zero = new(0);
        public static readonly Angle Angle180 = new(180);

        public double Degrees { get; } 
        public double Radians { get; }

        private Angle(double degrees)
        {
            Degrees = degrees;
            Radians = degrees * PiOver180;
        }

        public static Angle FromDegrees(double degrees)
        {
            return new Angle(degrees);
        }

        public static Angle FromRadians(double radians)
        {
            return new Angle(radians / PiOver180);
        }
    }
}
