using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands.RemindPassword
{
    public class RemindPasswordCommand : ICommand<RemindPasswordResult>
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
