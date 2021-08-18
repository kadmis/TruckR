using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;

namespace Auth.Domain.Data.Rules
{
    public class UserCannotBeInDeletedState : IBusinessRule
    {
        private readonly User _user;

        public UserCannotBeInDeletedState(User user)
        {
            _user = user;
        }

        public string Message => "User cannot be in deleted state for current operation.";

        public bool IsBroken()
        {
            return _user.DeletedDate.HasValue;
        }
    }
}
