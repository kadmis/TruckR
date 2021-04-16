using Auth.Domain.Data.ValueObjects;
using System;

namespace Auth.IntegrationEvents
{
    public class UserResetPasswordEvent : IntegrationEvent
    {
        public Email Email { get; }
        public Guid UserId { get; }
        public Guid ResetToken { get; }

        public UserResetPasswordEvent(Email email, Guid resetToken, Guid userId) : base()
        {
            Email = email;
            ResetToken = resetToken;
            UserId = userId;
        }
    }
}
