using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Exchanges;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Handlers.Exchanges;

public class UpdateExchangeHandler : IRequestHandler<UpdateExchangeRequest, UpdateExchangeResponse>
{
    private readonly IRepository<ExchangeEntity> _repository;

    public UpdateExchangeHandler(IRepository<ExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<UpdateExchangeResponse> Handle(UpdateExchangeRequest request, CancellationToken cancellationToken)
    {
        var item = new ExchangeEntity()
        {
            Id = request.Id,
            Closed = request.Closed,
            DateEnd = request.DateEnd,
            DateStart = request.DateStart,
            DirectionExchangeId = request.DirectionExchangeId,
            NameExecutor = request.NameExecutor,
            Symbol = request.Symbol
        };

        var update = await _repository.UpdateAsync(item);
        if (update == 0)
        {
            return new UpdateExchangeResponse() { Success = false };
        }

        return new UpdateExchangeResponse() { Data = item };
    }
}
