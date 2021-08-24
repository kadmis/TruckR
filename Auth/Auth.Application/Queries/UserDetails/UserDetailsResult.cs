using Auth.Domain.Security.Encryption;
using BuildingBlocks.Application.Models.Results;

namespace Auth.Application.Queries.UserDetails
{
    public class UserDetailsResult : IResult
    {
        public UserDetailsDTO Details { get; }
        public string Message { get; }
        public bool Successful { get; }

        private UserDetailsResult(UserDetailsDTO details, string message, bool successful)
        {
            Details = details;
            Message = message;
            Successful = successful;
        }

        public static UserDetailsResult Success(UserDetailsDTO details)
        {
            return new UserDetailsResult(details, string.Empty, true);
        }

        public static UserDetailsResult Fail(string message)
        {
            return new UserDetailsResult(null, message, false);
        }
    }

    public class UserDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public UserDetailsDTO Decrypt(IEncryptor decryptor)
        {
            FirstName = decryptor.Decrypt(FirstName);
            LastName = decryptor.Decrypt(LastName);
            Email = decryptor.Decrypt(Email);
            Phone = decryptor.Decrypt(Phone);

            return this;

        }
    }
}