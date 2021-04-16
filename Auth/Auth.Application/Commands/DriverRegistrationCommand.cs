using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;

namespace Auth.Application.Commands
{
    public class DriverRegistrationCommand : ICommand<RegistrationResult>
    {
        public string Username { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public string Password { get; }
        public string Email { get; }
        public string PhoneNumber { get; }

        public DriverRegistrationCommand(string username, string firstname, string lastname, string password, string email, string phoneNumber)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
