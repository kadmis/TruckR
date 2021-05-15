using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Drivers;
using Transport.Infrastructure.Persistence;

namespace Transport.Infrastructure.Messaging
{
    public class DriverActivatedEventHandler : IEventHandler<DriverActivatedEvent>
    {
        private readonly IUnitOfWork _uow;

        public DriverActivatedEventHandler(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task Handle(DriverActivatedEvent @event, CancellationToken cancellationToken = default)
        {
            var driver = Driver.Create(@event.Id, @event.Firstname + " " + @event.Lastname, @event.Email, @event.PhoneNumber);

            //_uow.DriversRepository.Add(driver);

            await _uow.Save(cancellationToken);
        }
    }
}
