using BuildingBlocks.Domain;

namespace Transport.Domain.Assignments
{
    public class Address : ValueObject
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        private Address()
        {
        }

        public static Address Create(string city, string street, string postalCode)
        {
            return new Address
            {
                City = city,
                Street = street,
                PostalCode = postalCode
            };
        }
    }
}
