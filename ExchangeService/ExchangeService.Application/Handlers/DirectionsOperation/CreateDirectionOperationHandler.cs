using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsOperation;
using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsOperation;

public class CreateDirectionOperationHandler : IRequestHandler<CreateDirectionOperationRequest, CreateDirectionOperationResponse>
{
    private readonly IRepository<DirectionOperationEntity> _repository;

    public CreateDirectionOperationHandler(IRepository<DirectionOperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<CreateDirectionOperationResponse> Handle(CreateDirectionOperationRequest request, CancellationToken cancellationToken)
    {
        var item = new DirectionOperationEntity()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            DirectionExchangeId = request.DirectionExchangeId
        };

        var create = await _repository.CreateAsync(item);
        if (create == 0)
        {
            return new CreateDirectionOperationResponse() { Success = false };
        }

        return new CreateDirectionOperationResponse() { Data = item.Id };
    }
}
