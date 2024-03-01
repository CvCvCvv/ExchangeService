using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsOperation;
using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsOperation;

public class UpdateDirectionOperationHandler : IRequestHandler<UpdateDirectionOperationRequest, UpdateDirectionOperationResponse>
{
    private readonly IRepository<DirectionOperationEntity> _repository;

    public UpdateDirectionOperationHandler(IRepository<DirectionOperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<UpdateDirectionOperationResponse> Handle(UpdateDirectionOperationRequest request, CancellationToken cancellationToken)
    {
        var item = new DirectionOperationEntity()
        {
            Id = request.Id,
            Name = request.Name,
            DirectionExchangeId = request.DirectionExchangeId
        };

        var create = await _repository.UpdateAsync(item);
        if (create == 0)
        {
            return new UpdateDirectionOperationResponse() { Success = false };
        }

        return new UpdateDirectionOperationResponse() { Data = item };
    }
}
