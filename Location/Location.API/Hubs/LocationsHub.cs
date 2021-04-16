using Location.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Location.API.Hubs
{
    public class LocationsHub : Hub
    {
        private readonly IMediator _mediator;

        public LocationsHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("Connected", $"{Context.ConnectionId} joined locations!");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("Disconnected", $"{Context.ConnectionId} left locations.");
        }

        public async Task SendLocation(SaveLocationCommand location)
        {
            await Clients.Others.SendAsync("SendLocation", location);
            await _mediator.Send(location);
        }
    }
}
