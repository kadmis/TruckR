using BuildingBlocks.API.SignalR.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transport.API.Hubs.ConnectedUsers;
using Transport.API.Hubs.Models;

namespace Transport.API.Hubs
{
    public class TransportHub : Hub
    {
        private readonly ConcurrentDictionary<string, ConnectedUsersService> _serviceMap;
        private readonly ConnectedDriversService _drivers;
        private readonly ConnectedDispatchersService _dispatchers;

        public TransportHub(ConnectedDriversService drivers, ConnectedDispatchersService dispatchers)
        {
            _drivers = drivers;
            _dispatchers = dispatchers;
            _serviceMap = new ConcurrentDictionary<string, ConnectedUsersService>();
            _serviceMap.TryAdd("Driver", drivers);
            _serviceMap.TryAdd("Dispatcher", dispatchers);
        }

        public async Task CreateAssignment(AssignmentCreated @event)
        {
            await Clients.Others.SendAsync("AssignmentCreated", @event);
        }

        public async Task TakeAssignment(AssignmentTaken @event)
        {
            await Clients.Others.SendAsync("AssignmentTaken", @event);
        }

        public async Task FinishAssignment(AssignmentFinished @event)
        {
            await Clients.Others.SendAsync("AssignmentFinished", @event);
        }

        public Task AddUserConnection(Guid userId, string role)
        {
            if (!string.IsNullOrWhiteSpace(role) && _serviceMap.TryGetValue(role, out ConnectedUsersService service))
                service.Add(userId, Context.ConnectionId);

            return Task.CompletedTask;
        }

        public Task RemoveUserConnection(Guid userId, string role)
        {
            if (!string.IsNullOrWhiteSpace(role) && _serviceMap.TryGetValue(role, out ConnectedUsersService service))
                service.Remove(userId, Context.ConnectionId);

            return Task.CompletedTask;
        }

        public async Task SendAssignmentFailed(string assignmentId, string driverId, string dispatcherId)
        {
            var sendAssignmentFailedTasks = new List<Task>();

            var driverConnections = _drivers.ConnectionsFor(Guid.Parse(driverId));
            var dispatcherConnections = _dispatchers.ConnectionsFor(Guid.Parse(dispatcherId));

            if (driverConnections != null)
                foreach (var connection in driverConnections.Connections)
                    sendAssignmentFailedTasks.Add(Clients.Client(connection).SendAsync("AssignmentFailed", new { AssignmentId = assignmentId }));

            if (dispatcherConnections != null)
                foreach (var connection in dispatcherConnections.Connections)
                    sendAssignmentFailedTasks.Add(Clients.Client(connection).SendAsync("AssignmentFailed", new { AssignmentId = assignmentId }));

            await Task.WhenAll(sendAssignmentFailedTasks);
        }

        public async Task SendAssignmentExpired(string assignmentId, string dispatcherId)
        {
            var sendAssignmentExpiredTasks = new List<Task>();

            var driverConnections = _drivers.AllConnections();
            var dispatcherConnections = _dispatchers.ConnectionsFor(Guid.Parse(dispatcherId));

            if (driverConnections != null)
                foreach (var driverConnection in driverConnections)
                    foreach (var connection in driverConnection.Connections)
                        sendAssignmentExpiredTasks.Add(Clients.Client(connection).SendAsync("AssignmentExpired", new { AssignmentId = assignmentId }));

            if (dispatcherConnections != null)
                foreach (var connection in dispatcherConnections.Connections)
                    sendAssignmentExpiredTasks.Add(Clients.Client(connection).SendAsync("AssignmentExpired", new { AssignmentId = assignmentId }));

            await Task.WhenAll(sendAssignmentExpiredTasks);
        }
    }
}
