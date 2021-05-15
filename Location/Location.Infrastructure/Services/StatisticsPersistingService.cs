using BuildingBlocks.Domain;
using Location.Domain.Repositories;
using Location.Infrastructure.Cache.Interfaces;
using Location.Infrastructure.Cache.Models;
using Location.Infrastructure.Services.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Infrastructure.Services
{
    public class StatisticsPersistingService : IStatisticsPersistingService
    {
        private readonly ILocationCacheService _cachedLocationsService;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILocationsToStatisticsConverter _locationsToStatisticsConverter;

        public StatisticsPersistingService(
            ILocationCacheService cachedLocationsService,
            IStatisticsRepository statisticsRepository, 
            ILocationsToStatisticsConverter locationsToStatisticsConverter)
        {
            _cachedLocationsService = cachedLocationsService;
            _statisticsRepository = statisticsRepository;
            _locationsToStatisticsConverter = locationsToStatisticsConverter;
        }

        public async Task Persist(CancellationToken cancellationToken = default)
        {
            for(int i=1;i<=7;i++)
            {
                var key = CachedLocation.GenerateKey(Clock.Now.AddHours(-i));
                var cachedLocations = await _cachedLocationsService.GetAll(key);

                if (cachedLocations.Any())
                {
                    var statistics = _locationsToStatisticsConverter.FromLocations(cachedLocations);

                    await _statisticsRepository.Add(statistics, cancellationToken);

                    await _cachedLocationsService.Remove(key);
                }
            }
        }
    }
}
