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
            var id = _context.HttpContext.UserId();
            var name = _context.HttpContext.UserName();
            var role = _context.HttpContext.UserRole();
            var authId = _context.HttpContext.UserAuthenticationId();

            return Identity.CreateIdentity(id.Value, name, role, authId.Value);
        }
    }

    internal static class HttpContextIdentityExtensions
    {
        public static Guid? UserId(this HttpContext context)
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

        public static string UserName(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string UserRole(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Role)?.Value;
        }

        public static Guid? UserAuthenticationId(this HttpContext context)
        {
            if (Guid.TryParse(context.User?.FindFirst(ClaimTypes.Sid)?.Value, out Guid id))
                return id;
            else
                return null;
        }
    }
}
