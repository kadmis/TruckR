using BuildingBlocks.Application.Queries;
using System;

namespace Auth.Application.Queries.UserDetails
{
    public class UserDetailsQuery : IQuery<UserDetailsResult>
    {
        public Guid? UserId { get; set; }
    }
}
