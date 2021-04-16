using Location.Infrastructure.Cache.Interfaces;
using Location.Infrastructure.Cache.Models;
using Location.Infrastructure.Services;
using ProtoBuf;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Location.Infrastructure.Cache
{
    public class RedisCacheService : ILocationCacheService
    {
        private readonly IConnectionMultiplexer _connection;
        private IDatabase Database => _connection.GetDatabase();

        public RedisCacheService(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        public Task Add(CachedLocation item)
        {
            var serializedValue = ProtoSerializer.Serialize(item);
            var key = CachedLocation.GenerateKey();

            return Database.ListRightPushAsync(key, serializedValue);
        }

        public async Task<List<CachedLocation>> GetAll(string key)
        {
            var items = new List<CachedLocation>();
            
            var values = await Database.ListRangeAsync(key);

            foreach(var value in values)
            {
                items.Add(Serializer.Deserialize<CachedLocation>(value));
            }

            return items;
        }

        public async Task Remove(string key)
        {
            await Database.KeyDeleteAsync(key);
        }
    }
}
