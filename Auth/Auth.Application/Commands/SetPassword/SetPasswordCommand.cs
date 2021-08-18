using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands.SetPassword
{
    public class SetPasswordCommand : ICommand<SetPasswordResult>
    {
        public Guid UserId { get; set; }
        public Guid ResetToken { get; set; }
        public string Password { get; set; }
    }
}
