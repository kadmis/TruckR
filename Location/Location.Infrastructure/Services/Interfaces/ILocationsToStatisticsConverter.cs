using Location.Domain.Entities;
using Location.Infrastructure.Cache.Models;
using System.Collections.Generic;

namespace Location.Infrastructure.Services.Interfaces
{
    public interface ILocationsToStatisticsConverter
    {
        IEnumerable<Statistic> FromLocations(List<CachedLocation> locations);
    }
}