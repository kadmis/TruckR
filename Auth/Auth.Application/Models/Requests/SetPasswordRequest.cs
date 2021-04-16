using System;

namespace Auth.Application.Models.Requests
{
    public class SetPasswordRequest
    {
        public Guid UserId { get; set; }
        public Guid ResetToken { get; set; }
        public string Password { get; set; }
    }
}
