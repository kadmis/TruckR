using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Data.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; }

        private PhoneNumber()
        {
        }
        public PhoneNumber(string number)
        {
            Number = number;
        }
    }
}
