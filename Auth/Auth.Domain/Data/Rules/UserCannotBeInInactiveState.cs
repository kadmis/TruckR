using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;

namespace Auth.Domain.Data.Rules
{
    public class UserCannotBeInInactiveState : IBusinessRule
    {
        private readonly User _user;

        public UserCannotBeInInactiveState(User user)
        {
            _user = user;
        }

        public string Message => "User cannot be in inactive state for current operation.";

        public bool IsBroken()
        {
            return !_user.Active;
        }
    }
}
