using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Exchanges;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Handlers.Exchanges;

public class CreateExchangeHandler : IRequestHandler<CreateExchangeRequest, CreateExchangeResponse>
{
    private readonly IRepository<ExchangeEntity> _repository;

    public CreateExchangeHandler(IRepository<ExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<CreateExchangeResponse> Handle(CreateExchangeRequest request, CancellationToken cancellationToken)
    {
        var exchange = new ExchangeEntity()
        {
            Id = Guid.NewGuid(),
            Closed = request.Closed,
            DateEnd = request.DateEnd,
            DateStart = request.DateStart,
            DirectionExchangeId = request.DirectionExchangeId,
            NameExecutor = request.NameExecutor,
            Symbol = request.Symbol
        };

        var create = await _repository.CreateAsync(exchange);
        if (create == 0)
        {
            return new CreateExchangeResponse() { Success = false };
        }

        return new CreateExchangeResponse() { Data = exchange.Id };
    }
}
