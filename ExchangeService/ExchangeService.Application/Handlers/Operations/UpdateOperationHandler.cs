using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Operations;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Handlers.Operations;

public class UpdateOperationHandler : IRequestHandler<UpdateOperationRequest, UpdateOperationResponse>
{
    private readonly IRepository<OperationEntity> _repository;

    public UpdateOperationHandler(IRepository<OperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<UpdateOperationResponse> Handle(UpdateOperationRequest request, CancellationToken cancellationToken)
    {
        var exchange = new OperationEntity()
        {
            Id = request.Id,
            OperationType = request.OperationType,
            DirectionOperationId = request.DirectionOperationId,
            Sum = request.Sum,
            Volume = request.Volume,
            ExchangeId = request.ExchangeId
        };

        var create = await _repository.UpdateAsync(exchange);
        if (create == 0)
        {
            return new UpdateOperationResponse() { Success = false };
        }

        return new UpdateOperationResponse() { Data = exchange };
    }
}
