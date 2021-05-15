using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Drivers
{
    public class Driver : IEntity<Guid>, IAggregateRoot
    {
        public Guid Id { get; private set; }

        private string _fullname;

        private string _email;

        private string _phoneNumber;

        private Driver()
        {
        }

        public static Driver Create(Guid id, string fullname, string email, string phonenumber)
        {
            return new Driver
            {
                Id = id,
                _fullname = fullname,
                _email = email,
                _phoneNumber = phonenumber
            };
        }
    }
}
