using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Employees
{
    public class Employee : IEntity<Guid>, IAggregateRoot
    {
        public Guid Id { get; private set; }

        private string _fullname;

        private string _email;

        private string _role;

        private Employee()
        {
        }

        public static Employee Create(Guid id, string fullname, string email, string role)
        {
            return new Employee
            {
                Id = id,
                _fullname = fullname,
                _email = email,
                _role = role
            };
        }
    }
}
