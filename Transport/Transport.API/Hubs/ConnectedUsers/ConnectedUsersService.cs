using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Transport.API.Hubs.ConnectedUsers
{
    public class ConnectedUsersService
    {
        private readonly ConcurrentDictionary<Guid, UserConnections> _userConnections = new();

        public void Add(Guid userId, string connection)
        {
            var exists = _userConnections.ContainsKey(userId);

            if (!exists)
                _userConnections.TryAdd(userId, new UserConnections());

            var connections = _userConnections.GetValueOrDefault(userId);

            connections.Add(connection);
        }

        public void Remove(Guid userId, string connection)
        {
            if (!_userConnections.ContainsKey(userId))
                return;

            var connections = _userConnections.GetValueOrDefault(userId);

            connections.Remove(connection);

            if (!connections.Connections.Any())
                _userConnections.Remove(userId, out _);
        }

        public UserConnections ConnectionsFor(Guid userId)
        {
            return _userConnections.GetValueOrDefault(userId);
        }


        public IEnumerable<UserConnections> AllConnections()
        {
            return _userConnections.Values;
        }
    }
}
