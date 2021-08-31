using BuildingBlocks.API.SignalR.Extensions;
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

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("UserDisconnected", Context.HubUserId());
            await base.OnDisconnectedAsync(exception);
        }

        [Authorize(Roles = "Driver")]
        public async Task ShareLocation(SaveLocationCommand location)
        {
            await Clients.Others.SendAsync("LocationShared", location);
            await _mediator.Send(location);
        }
    }
}
