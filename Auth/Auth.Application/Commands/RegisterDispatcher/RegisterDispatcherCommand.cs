using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands.RegisterDispatcher
{
    public class RegisterDispatcherCommand : ICommand<RegisterDispatcherResult>
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
