using BuildingBlocks.Application.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Transport.Infrastructure.Persistence;

namespace Transport.Infrastructure.Processing
{
    public class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork _uow;

        public UnitOfWorkPipelineBehavior(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = await next();

            await _uow.Save(cancellationToken);

            return result;
        }
    }
}
