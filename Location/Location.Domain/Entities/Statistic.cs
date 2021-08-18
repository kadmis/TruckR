using BuildingBlocks.Domain;
using Location.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Location.Domain.Entities
{
    public class Statistic : Entity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Distance DistanceDriven { get; private set; }
        public Speed AverageSpeed { get; private set; }
        public string Route { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        private Statistic(Guid userId, Distance distanceDriven, DateTime from, DateTime to, Coordinates[] coordinates)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            DistanceDriven = distanceDriven;
            AverageSpeed = Speed.Create(distanceDriven, to - from);
            Route = JsonSerializer.Serialize(coordinates);
            From = from;
            To = to;
        }

        public static Statistic Create(Guid userId, Distance distance, DateTime from, DateTime to, Coordinates[] coordinates)
        {
            return new Statistic(userId, distance, from, to, coordinates);
        }

        public List<Coordinates> ListRouteCoordinates()
        {
            return JsonSerializer.Deserialize<List<Coordinates>>(Route);
        }
    }
}
