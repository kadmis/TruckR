﻿using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands
{
    public class AuthenticationCommand : ICommand<AuthenticationResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
