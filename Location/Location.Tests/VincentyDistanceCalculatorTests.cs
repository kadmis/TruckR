using Location.Domain.Services;
using Location.Domain.ValueObjects;
using Location.Infrastructure.Services;
using Xunit;

namespace Location.Tests
{
    public class VincentyDistanceCalculatorTests
    {
        private readonly IDistanceCalculator vincentyDistance = new VincentyDistanceCalculator(Ellipsoid.WGS84);

        [Fact]
        public void Calculate_Distances_WGS84()
        {
            //Arrange

            //According to google maps, the distance sum of these coordinates is 360,25m
            var coordinates = new Coordinates[]
            {
                Coordinates.Create(50.39125287123264, 18.952261487581307),
                Coordinates.Create(50.392700495393356, 18.952474362851998),
                Coordinates.Create(50.3927432197405, 18.95259262689127),
                Coordinates.Create(50.392682903003944, 18.95301837743265),
                Coordinates.Create(50.39251451837493, 18.953649118975434),
                Coordinates.Create(50.39237126530931, 18.95411429086324),
                Coordinates.Create(50.39213250923805, 18.95459128915497),
                Coordinates.Create(50.39192391084437, 18.954894833522435)
            };

            //Act
            var distance = vincentyDistance.Calculate(coordinates);

            //Assert
            Assert.True(distance.Meters > 355 && distance.Meters < 365);
        }
    }
}
