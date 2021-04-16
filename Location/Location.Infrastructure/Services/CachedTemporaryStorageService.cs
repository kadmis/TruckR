using Location.Application.DTO;
using Location.Domain.Services;
using Location.Infrastructure.Cache.Interfaces;
using System.Threading.Tasks;

namespace Location.Infrastructure.Services
{
    public class CachedTemporaryStorageService : ITemporaryStorageService<LocationModel>
    {
        private readonly ILocationCacheService _cacheService;

        public CachedTemporaryStorageService(ILocationCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task AddAsync(LocationModel item)
        {
            return _cacheService.Add(new Cache.Models.CachedLocation(item.Latitude, item.Longitude, item.UserId));
        }
    }
}
