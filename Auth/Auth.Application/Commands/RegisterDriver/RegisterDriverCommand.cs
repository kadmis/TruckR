﻿using Auth.Application.Commands.RegisterDispatcher;
using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands.RegisterDriver
{
    public class RegisterDriverCommand : ICommand<RegisterDriverResult>
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
