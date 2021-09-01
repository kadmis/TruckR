using BuildingBlocks.API.SignalR.Extensions;
using Microsoft.AspNetCore.SignalR;
using Transport.API.Hubs.ConnectedUsers;

namespace Transport.API.Hubs.Extensions
{
    public static class HubContextExtensions
    {
        public static UserKey UserKey(this HubCallerContext context)
        {
            var id = context.HubUserId();
            if (id.HasValue)
                return new UserKey(id.Value, context.HubUserRole());
            else
                return null;
        }
    }
}
