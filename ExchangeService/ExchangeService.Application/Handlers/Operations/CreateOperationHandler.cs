using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Operations;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Handlers.Operations;

public class CreateOperationHandler : IRequestHandler<CreateOperationRequest, CreateOperationResponse>
{
    private readonly IRepository<OperationEntity> _repository;

    public CreateOperationHandler(IRepository<OperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<CreateOperationResponse> Handle(CreateOperationRequest request, CancellationToken cancellationToken)
    {
        var exchange = new OperationEntity()
        {
            Id = Guid.NewGuid(),
            OperationType = request.OperationType,
            DirectionOperationId = request.DirectionOperationId,
            Sum = request.Sum,
            Volume = request.Volume,
            ExchangeId = request.ExchangeId
        };

        var create = await _repository.CreateAsync(exchange);
        if (create == 0)
        {
            return new CreateOperationResponse() { Success = false };
        }

        return new CreateOperationResponse() { Data = exchange.Id };
    }
}
