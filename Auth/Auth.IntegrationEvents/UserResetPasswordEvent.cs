using System;

namespace Auth.IntegrationEvents
{
    public class UserResetPasswordEvent : IntegrationEvent
    {
        public string Email { get; }
        public Guid UserId { get; }
        public Guid ResetToken { get; }

        public UserResetPasswordEvent(string email, Guid resetToken, Guid userId)
        {
            Email = email;
            ResetToken = resetToken;
            OccuredOn = DateTime.Now;
            UserId = userId;
        }
    }
}
