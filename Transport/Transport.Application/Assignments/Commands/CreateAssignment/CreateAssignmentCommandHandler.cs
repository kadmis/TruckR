using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Application.Assignments.Queries.DocumentsThisMonth;
using Transport.Application.Files;
using Transport.Domain.Assignments;

namespace Transport.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentCommandHandler : ICommandHandler<CreateAssignmentCommand, CreateAssignmentResult>
    {
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly IAssignmentBuilder _assignmentBuilder;
        private readonly IFilesStorage _filesStorage;
        private readonly IMediator _mediator;

        public CreateAssignmentCommandHandler(
            IIdentityAccessor identityAccessor,
            IAssignmentsRepository assignmentsRepository,
            IAssignmentBuilder assignmentBuilder,
            IFilesStorage filesStorage, 
            IMediator mediator)
        {
            _identityAccessor = identityAccessor;
            _assignmentsRepository = assignmentsRepository;
            _assignmentBuilder = assignmentBuilder;
            _filesStorage = filesStorage;
            _mediator = mediator;
        }

        public async Task<CreateAssignmentResult> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _identityAccessor.UserIdentity();

                var dispatcher = user.UserId;

                var documentsThisMonthResult = await _mediator.Send(new DocumentsThisMonthQuery(), cancellationToken);

                if (!documentsThisMonthResult.Successful)
                    return CreateAssignmentResult.Fail(documentsThisMonthResult.Message);

                var assignment = _assignmentBuilder
                    .AddBasicInformation(request.Title, request.Description, request.Deadline)
                    .AddDispatcher(dispatcher)
                    .AddTransportDocument(request.Document.FileName, documentsThisMonthResult.Count.Value)
                    .AddDestination(request.DestinationCountry, request.DestinationCity, request.DestinationStreet, request.DestinationPostalCode)
                    .AddStartingPoint(request.StartingCountry, request.StartingCity, request.StartingStreet, request.StartingPostalCode)
                    .Build();

                var addedFile = await _filesStorage.Save(
                    assignment.DocumentId.ToString(),
                    request.Document.FileName,
                    request.Document.Length,
                    request.Document.ContentType,
                    request.Document.OpenReadStream(),
                    cancellationToken);

                _assignmentsRepository.Add(assignment);

                return CreateAssignmentResult.Success(assignment.Id);
            }
            catch (Exception ex)
            {
                return CreateAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
