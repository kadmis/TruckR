using MediatR;

namespace BuildingBlocks.Application.Commands
{
    public interface ICommand<T> : IRequest<T>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
