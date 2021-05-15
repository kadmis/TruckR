using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands
{
    public class RegisterDriverCommand : ICommand<RegistrationResult>
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
