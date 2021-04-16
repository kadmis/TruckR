using System;

namespace Auth.Domain.Specifications.UsernameSpecifications.Interfaces
{
    public interface IUsernameExistsOnOtherUsers : IUsernameSpecification
    {
        IUsernameExistsOnOtherUsers Setup(Guid currentUserId);
    }
}
