using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsExchange;

public class CreateDirectionExchangeHandler : IRequestHandler<CreateDirectionExchangeRequest, CreateDirectionExchangeResponse>
{
    private readonly IRepository<DirectionExchangeEntity> _repository;

    public CreateDirectionExchangeHandler(IRepository<DirectionExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<CreateDirectionExchangeResponse> Handle(CreateDirectionExchangeRequest request, CancellationToken cancellationToken)
    {
        var item = new DirectionExchangeEntity()
        {
            Id = Guid.NewGuid(),
            DateStart = request.DateStart,
            Name = request.Name
        };

        var create = await _repository.CreateAsync(item);
        if (create == 0)
        {
            return new CreateDirectionExchangeResponse() { Success = false };
        }

        return new CreateDirectionExchangeResponse() { Data = item.Id };
    }
}
