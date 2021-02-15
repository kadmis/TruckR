using System;

namespace Auth.API.Infrastructure.Models.Requests
{
    public class SetPasswordRequest
    {
        public Guid UserId { get; set; }
        public Guid ResetToken { get; set; }
        public string Password { get; set; }
    }
}
