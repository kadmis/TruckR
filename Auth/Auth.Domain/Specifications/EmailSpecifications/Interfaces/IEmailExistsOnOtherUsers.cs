using System;

namespace Auth.Domain.Specifications.EmailSpecifications.Interfaces
{
    public interface IEmailExistsOnOtherUsers : IEmailSpecification
    {
        public IEmailExistsOnOtherUsers Setup(Guid currentUserId);
    }
}
