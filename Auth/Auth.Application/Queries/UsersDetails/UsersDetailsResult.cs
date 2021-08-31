using Auth.Application.Queries.UserDetails;
using Auth.Domain.Security.Encryption;
using BuildingBlocks.Application.Models.Results;
using System.Collections.Generic;

namespace Auth.Application.Queries.UsersDetails
{
    public class UsersDetailsResult : IResult<IEnumerable<UserDetailsDTO>>
    {
        public IEnumerable<UserDetailsDTO> Data { get; private set; }

        public string Message { get; private set; }

        public bool Successful { get; private set; }

        private UsersDetailsResult()
        {
        }

        public static UsersDetailsResult Success(IEnumerable<UserDetailsDTO> data)
        {
            return new UsersDetailsResult
            {
                Successful = true,
                Message = string.Empty,
                Data = data
            };
        }

        public static UsersDetailsResult Fail(string message)
        {
            return new UsersDetailsResult
            {
                Successful = false,
                Message = message,
            };
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
