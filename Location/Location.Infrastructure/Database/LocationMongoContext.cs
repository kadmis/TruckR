using Location.Domain.Entities;
using MongoDB.Driver;

namespace Location.Infrastructure.Database
{
    public class LocationMongoContext
    {
        private readonly IMongoClient _client;

        private IMongoDatabase Database => _client.GetDatabase("LocationDB");
        public IMongoCollection<Statistic> Statistics => Database.GetCollection<Statistic>("Statistics");

        public LocationMongoContext(IMongoClient client)
        {
            _client = client;
        }
    }
}
