using Auth.API.Infrastructure.Models.CommandsResults;

namespace Auth.API.Infrastructure.Commands
{
    public class RemindPasswordCommand : ICommand<RemindPasswordResult>
    {
        public RemindPasswordCommand(string email, string username)
        {
            Email = email;
            Username = username;
        }

        public string Email { get; }
        public string Username { get; }

    }
}
