using Location.API.Infrastructure.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Infrastructure.Hubs
{
    public class LocationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("Connected", $"{Context.ConnectionId} joined locations!");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("Disconnected", $"{Context.ConnectionId} left locations.");
        }

        public async Task SendLocation(LocationModel location)
        {
            await Clients.Others.SendAsync("SendLocation", location);
        }
    }
}
