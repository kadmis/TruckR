using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands.RemindUsername
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
