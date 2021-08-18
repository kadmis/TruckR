using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Handlers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Groups;
using Transport.Infrastructure.Persistence;

namespace Transport.Infrastructure.InternalCommands.CreateGroup
{
    public class CreateGroupCommand : ICommand
    {
        public Guid DispatcherId { get; }

        public CreateGroupCommand(DispatcherActivatedEvent @event)
        {
            DispatcherId = @event.UserId;
        }
    }

    public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand>
    {
        private readonly IUnitOfWork _uow;

        public CreateGroupCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = TransportGroup.Create(request.DispatcherId);

            _uow.TransportGroupsRepository.Add(group);

            await _uow.Save(cancellationToken);

            return Unit.Value;
        }
    }
}
