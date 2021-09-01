using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transport.API.Hubs.ConnectedUsers
{
    public class UserConnections
    {
        private readonly List<string> _connections;

        public IEnumerable<string> Connections => _connections.AsReadOnly();

        public UserConnections()
        {
            _connections = new();
        }

        public void Add(string connection)
        {
            lock(_connections)
            {
                _connections.Add(connection);
            }
        }

        public void Remove(string connection)
        {
            lock(_connections)
            {
                _connections.Remove(connection);
            }
        }
    }
}
