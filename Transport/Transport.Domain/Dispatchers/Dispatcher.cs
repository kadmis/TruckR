using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Dispatchers
{
    public class Dispatcher : IEntity<Guid>, IAggregateRoot
    {
        public Guid Id { get; private set; }

        private string _fullname;

        private string _email;

        private string _phoneNumber;

        private Dispatcher()
        {
        }

        public static Dispatcher Create(Guid id, string fullname, string email, string phonenumber)
        {
            return new Dispatcher
            {
                Id = id,
                _fullname = fullname,
                _email = email,
                _phoneNumber = phonenumber
            };
        }
    }
}
