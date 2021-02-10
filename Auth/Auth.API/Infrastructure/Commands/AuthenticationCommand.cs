using Auth.API.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Commands
{
    public class AuthenticationCommand : ICommand<AuthenticationResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
