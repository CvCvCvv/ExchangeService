using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsExchange;

public class GetDirectionsExchangeHandler : IRequestHandler<GetDirectionsExchangeRequest, GetDirectionsExchangeResponse>
{
    private readonly IRepository<DirectionExchangeEntity> _repository;

    public GetDirectionsExchangeHandler(IRepository<DirectionExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetDirectionsExchangeResponse> Handle(GetDirectionsExchangeRequest request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAsync();

        if (!items.Any())
        {
            return new GetDirectionsExchangeResponse();
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            items = items.Where(w => w.Name == request.Name);
        }

        if (request.DateStart != null)
        {
            items = items.Where(w => w.DateStart == request.DateStart);
        }

        return new GetDirectionsExchangeResponse() { Data = items.ToArray() };
    }
}
