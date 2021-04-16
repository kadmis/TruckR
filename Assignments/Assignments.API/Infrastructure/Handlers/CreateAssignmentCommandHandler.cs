using Assignments.API.Infrastructure.Commands;
using Assignments.API.Infrastructure.Models.Results;
using Assignments.Domain.Data.Aggregates;
using Assignments.Domain.Data.Entities;
using Assignments.Domain.Data.ValueObjects;
using Assignments.Domain.Persistence;
using BuildingBlocks.API.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignments.API.Infrastructure.Handlers
{
    public class CreateAssignmentCommandHandler : ICommandHandler<CreateAssignmentCommand, CreateAssignmentResult>
    {
        private readonly IUnitOfWork _uow;

        public CreateAssignmentCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CreateAssignmentResult> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {

                return CreateAssignmentResult.Success();
            }
            catch(Exception ex)
            {
                return CreateAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
