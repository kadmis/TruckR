using BuildingBlocks.Domain;

namespace Transport.Domain.Assignments
{
    public class Address : ValueObject
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        private Address()
        {
        }

        public static Address Create(string country, string city, string street, string postalCode)
        {
            return new Address
            {
                Country = country,
                City = city,
                Street = street,
                PostalCode = postalCode
            };
        }
    }
}
