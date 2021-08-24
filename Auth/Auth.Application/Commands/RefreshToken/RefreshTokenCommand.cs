using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands.RefreshToken
{
    public class RefreshTokenCommand : ICommand<RefreshTokenResult>
    {
        public Guid RefreshToken { get; set; }
    }
}
