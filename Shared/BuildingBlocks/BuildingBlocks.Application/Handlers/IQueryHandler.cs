using BuildingBlocks.Application.Queries;
using MediatR;

namespace BuildingBlocks.Application.Handlers
{
    public interface IQueryHandler<Query, Result> : IRequestHandler<Query, Result> where Query : IQuery<Result>
    {
    }
}
