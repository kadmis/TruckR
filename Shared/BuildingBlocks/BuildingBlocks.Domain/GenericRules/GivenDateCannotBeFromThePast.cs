using System;

namespace BuildingBlocks.Domain.GenericRules
{
    public class GivenDateCannotBeFromThePast : IBusinessRule
    {
        private readonly string _fieldName;
        private readonly DateTime _date;

        public GivenDateCannotBeFromThePast(string fieldName, DateTime date)
        {
            _fieldName = fieldName;
            _date = date;
        }

        public string Message => $"{_fieldName} cannot be from the past.";

        public bool IsBroken()
        {
            return _date < Clock.Now;
        }
    }
}
