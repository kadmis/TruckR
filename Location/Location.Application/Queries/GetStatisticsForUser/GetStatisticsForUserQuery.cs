using BuildingBlocks.Application.Queries;
using Location.Application.DTO.Filters;
using System;

namespace Location.Application.Queries.GetStatisticsForUser
{
    public class GetStatisticsForUserQuery : IQuery<GetStatisticsForUserQueryResult>
    {
        public Guid UserId { get; }
        public StatisticsFilter Filter { get; }

        public GetStatisticsForUserQuery(Guid userId, StatisticsFilter filter)
        {
            UserId = userId;
            Filter = filter;
        }
    }
}
