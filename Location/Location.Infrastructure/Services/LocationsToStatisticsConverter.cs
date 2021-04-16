using Location.Domain.Entities;
using Location.Domain.Services;
using Location.Infrastructure.Cache.Models;
using Location.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Location.Infrastructure.Services
{
    public class LocationsToStatisticsConverter : ILocationsToStatisticsConverter
    {
        private readonly IDistanceCalculator _distanceCalculator;

        public LocationsToStatisticsConverter(IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        public IEnumerable<Statistic> FromLocations(List<CachedLocation> locations)
        {
            var statistics = new List<Statistic>();

            foreach(var group in locations.GroupBy(x => x.UserId))
            {
                var ordered = group.OrderBy(x => x.TimeStamp);

                var coordinates = ordered.Select(x => x.ToCoordinates()).ToArray();
                var distance = _distanceCalculator.Calculate(coordinates);
                var from = ordered.Min(x => x.TimeStamp);
                var to = ordered.Max(x => x.TimeStamp);

                statistics.Add(Statistic.Create(group.Key, distance, from, to, coordinates));
            }

            return statistics;
        }
    }
}
