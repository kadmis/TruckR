using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Models.Requests
{
    public class RemindPasswordRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
