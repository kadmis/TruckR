using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands
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
