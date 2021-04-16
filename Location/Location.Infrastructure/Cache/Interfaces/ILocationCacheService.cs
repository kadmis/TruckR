using Location.Infrastructure.Cache.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Location.Infrastructure.Cache.Interfaces
{
    public interface ILocationCacheService
    {
        Task Add(CachedLocation item);
        Task<List<CachedLocation>> GetAll(string key);
        Task Remove(string key);
    }
}
