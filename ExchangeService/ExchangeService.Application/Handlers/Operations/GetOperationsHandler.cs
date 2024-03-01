using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Operations;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Handlers.Operations;

public class GetOperationsHandler : IRequestHandler<GetOperationsRequest, GetOperationsResponse>
{
    private readonly IRepository<OperationEntity> _repository;

    public GetOperationsHandler(IRepository<OperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetOperationsResponse> Handle(GetOperationsRequest request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAsync();

        if (!items.Any())
        {
            return new GetOperationsResponse();
        }

        if (request.OperationType != null)
        {
            items = items.Where(w => w.OperationType == request.OperationType);
        }

        if (request.DirectionOperationId != null)
        {
            items = items.Where(w => w.DirectionOperationId == request.DirectionOperationId);
        }

        return new GetOperationsResponse() { Data = items.ToArray() };
    }
}
