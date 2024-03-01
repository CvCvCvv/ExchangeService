using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Exchanges;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Handlers.Exchanges;

public class GetExchangesHandler : IRequestHandler<GetExchangesRequest, GetExchangesResponse>
{
    private readonly IRepository<ExchangeEntity> _repository;

    public GetExchangesHandler(IRepository<ExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetExchangesResponse> Handle(GetExchangesRequest request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAsync();

        if (!items.Any())
        {
            return new GetExchangesResponse();
        }

        if (!string.IsNullOrEmpty(request.NameExecutor))
        {
            items = items.Where(w => w.NameExecutor == request.NameExecutor);
        }

        if (!string.IsNullOrEmpty(request.Symbol))
        {
            items = items.Where(w => w.Symbol == request.Symbol);
        }

        if (request.Closed != null)
        {
            items = items.Where(w => w.Closed == request.Closed);
        }

        if (request.DirectionExchangeId != null)
        {
            items = items.Where(w => w.DirectionExchangeId == request.DirectionExchangeId);
        }

        if (request.DateStart != null)
        {
            items = items.Where(w => w.DateStart == request.DateStart);
        }

        if (request.DateEnd != null)
        {
            items = items.Where(w => w.DateEnd == request.DateEnd);
        }

        return new GetExchangesResponse() { Data = items.ToArray() };
    }
}
