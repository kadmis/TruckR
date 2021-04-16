using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Location.Application.Queries
{
    public record GetStatisticsForUserQueryItem(double DistanceDriven, double AverageSpeed, DateTime DateFrom, DateTime DateTo);
    public class GetStatisticsForUserQueryResult
    {
        public IEnumerable<GetStatisticsForUserQueryItem> Items { get; }
        public GetStatisticsForUserQueryResult(IEnumerable<Statistic> statistics)
        {
            Items = statistics.Select(x => new GetStatisticsForUserQueryItem(x.DistanceDriven.Meters, x.AverageSpeed.MetersPerSecond, x.From, x.To));
        }
    }
}