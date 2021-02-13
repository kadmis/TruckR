using Auth.API.Infrastructure.Models.CommandsResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Commands
{
    public class SetPasswordCommand : ICommand<SetPasswordResult>
    {
        public Guid UserId { get; }
        public Guid ResetToken { get; }
        public string Password { get; }

        public SetPasswordCommand(Guid userId, Guid resetToken, string password)
        {
            UserId = userId;
            ResetToken = resetToken;
            Password = password;
        }
    }
}
