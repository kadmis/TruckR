using System;

namespace Transport.API.Hubs.ConnectedUsers
{
    public record UserKey
    {
        public UserKey(Guid id, string role)
        {
            Id = id;
            Role = role;
        }

        public Guid Id { get; init; }
        public string Role { get; init; }

        public static UserKey ForDriver(Guid id)
        {
            return new UserKey(id, "Driver");
        }
        public static UserKey ForDispatcher(Guid id)
        {
            return new UserKey(id, "Dispatcher");
        }
    }
}
