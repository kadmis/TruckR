using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;

namespace Auth.Domain.Data.Rules
{
    public class UserCannotBeInActiveState : IBusinessRule
    {
        private readonly User _user;

        public UserCannotBeInActiveState(User user)
        {
            _user = user;
        }

        public string Message => "User cannot be in active state for this operation.";

        public bool IsBroken()
        {
            return _user.Active;
        }
    }
}
