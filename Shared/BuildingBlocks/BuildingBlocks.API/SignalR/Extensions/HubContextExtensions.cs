using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;

namespace BuildingBlocks.API.SignalR.Extensions
{
    public static class HubContextExtensions
    {
        public static Guid? HubUserId(this HubCallerContext context)
        {
            if (Guid.TryParse(context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
            {
                return userId;
            }
            else
            {
                return null;
            }
        }

        public static string HubUserRole(this HubCallerContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
