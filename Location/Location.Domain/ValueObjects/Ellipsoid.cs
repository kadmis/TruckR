using BuildingBlocks.Domain;

namespace Location.Domain.ValueObjects
{
    public class Ellipsoid : ValueObject
    {
        static public readonly Ellipsoid WGS84 = FromAAndInverseF(6378137.0, 298.257223563);
        static public readonly Ellipsoid GRS80 = FromAAndInverseF(6378137.0, 298.257222101);
        static public readonly Ellipsoid GRS67 = FromAAndInverseF(6378160.0, 298.25);
        static public readonly Ellipsoid ANS = FromAAndInverseF(6378160.0, 298.25);
        static public readonly Ellipsoid Clarke1880 = FromAAndInverseF(6378249.145, 293.465);
        static public Ellipsoid FromAAndInverseF(double semiMajor, double inverseFlattening)
        {
            double f = 1.0 / inverseFlattening;
            double b = (1.0 - f) * semiMajor;
            return new Ellipsoid(semiMajor, b, f, inverseFlattening);
        }

        public double SemiMajorAxis { get; } 
        public double SemiMinorAxis { get; }
        public double Flattening { get; }
        public double InverseFlattening { get; }

        private Ellipsoid(double semiMajorAxis, double semiMinorAxis, double flattening, double inverseFlattening)
        {
            SemiMajorAxis = semiMajorAxis;
            SemiMinorAxis = semiMinorAxis;
            Flattening = flattening;
            InverseFlattening = inverseFlattening;
        }
    }
}
