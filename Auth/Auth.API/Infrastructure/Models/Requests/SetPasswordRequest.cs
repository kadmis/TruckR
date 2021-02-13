using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Models.Requests
{
    public class SetPasswordRequest
    {
        public Guid ResetToken { get; set; }
        public string Password { get; set; }
    }
}
