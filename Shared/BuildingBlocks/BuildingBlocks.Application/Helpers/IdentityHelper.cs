using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace BuildingBlocks.Application.Helpers
{
    public static class IdentityHelper
    {
        public static Identity UserIdentity(this HttpContext context)
        {
            var id = context.GetId();
            var name = context.GetName();

            if (id == null || id == Guid.Empty || string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("No valid user found");
            }

            return Identity.CreateIdentity(id.Value, name);
        }

        private static Guid? GetId(this HttpContext context)
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

        private static string GetName(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }

    public class Identity
    {
        public Guid UserId { get; }
        public string Name { get; }

        private Identity()
        {

        }
        private Identity(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        public static Identity CreateIdentity(Guid userId, string name)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("Invalid user id");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid user name");
            }

            return new Identity(userId, name);
        }
    }
}
