using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsExchange;

public class GetDirectionExchangeHandler : IRequestHandler<GetDirectionExchangeRequest, GetDirectionExchangeByIdResponse>
{
    private readonly IRepository<DirectionExchangeEntity> _repository;

    public GetDirectionExchangeHandler(IRepository<DirectionExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetDirectionExchangeByIdResponse> Handle(GetDirectionExchangeRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FindByIdAsync(request.DirectionExchangeId);
        if (item == null)
        {
            return new GetDirectionExchangeByIdResponse() { Success = false };
        }

        return new GetDirectionExchangeByIdResponse() { Data = item };
    }
}
