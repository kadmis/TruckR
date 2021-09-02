using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Infrastructure.Persistence;

namespace Transport.Infrastructure.InternalCommands.AddDriverToGroup
{
    public class AddDriverToGroupCommand : ICommand
    {
        public Guid DriverId { get; }

        public AddDriverToGroupCommand(DriverActivatedEvent @event)
        {
            DriverId = @event.UserId;
        }
    }

    public class AddDriverToGroupCommandHandler : IRequestHandler<AddDriverToGroupCommand>
    {
        private readonly IUnitOfWork _uow;

        public AddDriverToGroupCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(
            AddDriverToGroupCommand request, 
            CancellationToken cancellationToken)
        {
            var group = await _uow.TransportGroupsRepository
                .FindGroupWithFreeSpots(cancellationToken);

            group.AddDriver(request.DriverId);

            await _uow.Save(cancellationToken);

            return Unit.Value;
        }
    }
}
