using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;

namespace Auth.Domain.Data.Rules
{
    public class UserHasToExist : IBusinessRule
    {
        private readonly User _user;

        public UserHasToExist(User user)
        {
            _user = user;
        }

        public string Message => "User doesn't exist.";

        public bool IsBroken()
        {
            return _user == null;
        }
    }
}
