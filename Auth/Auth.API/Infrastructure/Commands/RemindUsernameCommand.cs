using Auth.API.Infrastructure.Models.CommandsResults;

namespace Auth.API.Infrastructure.Commands
{
    public class RemindUsernameCommand : ICommand<RemindUsernameResult>
    {
        public string Email { get; }

        public RemindUsernameCommand(string email)
        {
            Email = email;
        }
    }
}
