﻿using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands.RefreshToken
{
    public class RefreshTokenCommand : ICommand<AuthenticationResult>
    {
        public Guid RefreshToken { get; set; }
    }
}