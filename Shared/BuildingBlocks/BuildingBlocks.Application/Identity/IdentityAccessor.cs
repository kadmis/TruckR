using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace BuildingBlocks.Application.Identity
{
    public class IdentityAccessor : IIdentityAccessor
    {
        private readonly IHttpContextAccessor _context;

        public IdentityAccessor(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Identity UserIdentity()
        {
            var id = _context.HttpContext.GetId();
            var name = _context.HttpContext.GetName();
            var role = _context.HttpContext.GetRole();

            return Identity.CreateIdentity(id.Value, name, role);
        }
    }

    internal static class HttpContextIdentityExtensions
    {
        public static Guid? GetId(this HttpContext context)
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

        public static string GetName(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string GetRole(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
