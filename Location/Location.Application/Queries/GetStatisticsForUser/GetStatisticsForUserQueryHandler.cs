using BuildingBlocks.Application.Handlers;
using Location.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Application.Queries.GetStatisticsForUser
{
    public class GetStatisticsForUserQueryHandler : IQueryHandler<GetStatisticsForUserQuery, GetStatisticsForUserQueryResult>
    {
        private readonly IStatisticsReadOnlyRepository _statisticsRepository;

        public GetStatisticsForUserQueryHandler(IStatisticsReadOnlyRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }
        public async Task<GetStatisticsForUserQueryResult> Handle(GetStatisticsForUserQuery request, CancellationToken cancellationToken)
        {
            return new GetStatisticsForUserQueryResult(await _statisticsRepository.GetForUser(request.UserId, cancellationToken));
        }
    }
}
