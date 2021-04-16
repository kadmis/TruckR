using BuildingBlocks.Domain;
using System;

namespace Location.Domain.ValueObjects
{
    public class Speed : ValueObject
    {
        public double MetersPerSecond { get; }
        private Speed(double value)
        {
            MetersPerSecond = value;
        }

        public static Speed Create(Distance distance, TimeSpan timeSpan)
        {
            return new Speed(Math.Round(distance.Meters / timeSpan.TotalSeconds, 2));
        }
        public static Speed Create(double value) 
        {
            return new Speed(value);
        }
    }
}
