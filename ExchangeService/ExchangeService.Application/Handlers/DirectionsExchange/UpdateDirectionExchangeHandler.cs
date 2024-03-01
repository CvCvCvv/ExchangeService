using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsExchange;

public class UpdateDirectionExchangeHandler : IRequestHandler<UpdateDirectionExchangeRequest, UpdateDirectionExchangeResponse>
{
    private readonly IRepository<DirectionExchangeEntity> _repository;

    public UpdateDirectionExchangeHandler(IRepository<DirectionExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<UpdateDirectionExchangeResponse> Handle(UpdateDirectionExchangeRequest request, CancellationToken cancellationToken)
    {
        var item = new DirectionExchangeEntity()
        {
            Id = request.Id,
            DateStart = request.DateStart,
            Name = request.Name           
        };

        var create = await _repository.UpdateAsync(item);
        if (create == 0)
        {
            return new UpdateDirectionExchangeResponse() { Success = false };
        }

        return new UpdateDirectionExchangeResponse() { Data = item };
    }
}
