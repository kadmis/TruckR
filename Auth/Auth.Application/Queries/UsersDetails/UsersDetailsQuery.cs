using BuildingBlocks.Application.Queries;
using System;

namespace Auth.Application.Queries.UsersDetails
{
    public class UsersDetailsQuery : IQuery<UsersDetailsResult>
    {
        public Guid[] Ids { get; set; }
    }
}
