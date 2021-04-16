using Location.Domain.ValueObjects;

namespace Location.Domain.Services
{
    /// <summary>
    /// Interface for calculating the sum of distance between given coordinates. The order of given coordinates matter.
    /// </summary>
    public interface IDistanceCalculator
    {
        Distance Calculate(params Coordinates[] coordinates);
    }
}
