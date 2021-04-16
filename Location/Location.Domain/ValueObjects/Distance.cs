using BuildingBlocks.Domain;
using System;

namespace Location.Domain.ValueObjects
{
    public class Distance : ValueObject
    {
        public double Meters { get; }

        private Distance(double meters)
        {
            Meters = Math.Round(meters, 2);
        }

        public static Distance Create(double meters)
        {
            return new Distance(meters);
        }
    }
}
