using Location.Domain.Entities;
using Location.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace Location.Infrastructure.Database.MongoMappers
{
    public class StatisticMongoMap
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<Distance>(map =>
            {
                map.MapMember(x => x.Meters).SetIsRequired(true);
            });
            BsonClassMap.RegisterClassMap<Speed>(map =>
            {
                map.MapMember(x => x.MetersPerSecond).SetIsRequired(true);
            });
            BsonClassMap.RegisterClassMap<Statistic>(map =>
            {
                map.MapIdMember(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                map.MapMember(x => x.UserId).SetIsRequired(true).SetSerializer(new GuidSerializer(BsonType.String));
                map.MapMember(x => x.DistanceDriven).SetIsRequired(true);
                map.MapMember(x => x.AverageSpeed).SetIsRequired(true);
                map.MapMember(x => x.Route).SetIsRequired(true);
                map.MapMember(x => x.From).SetIsRequired(true).SetSerializer(new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime));
                map.MapMember(x => x.To).SetIsRequired(true).SetSerializer(new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime));
            });
        }
    }
}
