using System;

namespace Auth.Domain.Exceptions.Specifications
{
    public class ArgumentsNotProvidedException : Exception
    {
        public ArgumentsNotProvidedException(string specificationName):base($"Required arguments have not been provided for {specificationName}")
        {
        }
    }
}
