using Auth.API.Infrastructure.Models.CommandsResults;

namespace Auth.API.Infrastructure.Commands
{
    public class RegistrationCommand : ICommand<RegistrationResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public RegistrationCommand(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
