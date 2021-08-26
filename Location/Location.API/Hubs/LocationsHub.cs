using Location.Application.Commands.SaveLocation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Location.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        //[Authorize(Roles = "Driver")]
        public async Task ShareLocation(SaveLocationCommand location)
        {
            await Clients.Others.SendAsync("LocationShared", location);
            await _mediator.Send(location);
        }
    }
}
