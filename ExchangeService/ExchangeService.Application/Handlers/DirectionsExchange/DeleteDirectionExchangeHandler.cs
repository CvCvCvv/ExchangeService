using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsExchange;

public class DeleteDirectionExchangeHandler : IRequestHandler<DeleteDirectionExchangeRequest, DeleteDirectionExchangeResponse>
{
    private readonly IRepository<DirectionExchangeEntity> _repository;

    public DeleteDirectionExchangeHandler(IRepository<DirectionExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<DeleteDirectionExchangeResponse> Handle(DeleteDirectionExchangeRequest request, CancellationToken cancellationToken)
    {
        var delete = await _repository.RemoveAsync(request.DirectionExchangeId);
        if (delete == 0)
        {
            return new DeleteDirectionExchangeResponse() { Success = false };
        }

        return new DeleteDirectionExchangeResponse() { Data = request.DirectionExchangeId };
    }
}
