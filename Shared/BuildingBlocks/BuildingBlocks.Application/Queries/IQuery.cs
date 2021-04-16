using MediatR;

namespace BuildingBlocks.Application.Queries
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
