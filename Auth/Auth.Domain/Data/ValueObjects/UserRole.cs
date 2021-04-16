using BuildingBlocks.Domain;

namespace Auth.Domain.Data.ValueObjects
{
    public class UserRole : ValueObject
    {
        public static UserRole Admin => new(nameof(Admin));
        public static UserRole Dispatcher => new(nameof(Dispatcher));
        public static UserRole Driver => new(nameof(Driver));

        public string Value { get; }

        private UserRole(string value)
        {
            Value = value;
        }
    }
}
