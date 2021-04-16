using BuildingBlocks.Application.Commands;
using MediatR;

namespace BuildingBlocks.Application.Handlers
{
    public interface ICommandHandler<Command, Result> : IRequestHandler<Command, Result> where Command : ICommand<Result>
    {
    }

    public interface ICommandHandler<Command> : IRequestHandler<Command> where Command : ICommand
    {
    }
}
