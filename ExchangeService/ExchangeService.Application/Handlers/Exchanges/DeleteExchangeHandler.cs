using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Exchanges;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Handlers.Exchanges;

public class DeleteExchangeHandler : IRequestHandler<DeleteExchangeRequest, DeleteExchangeResponse>
{
    private readonly IRepository<ExchangeEntity> _repository;

    public DeleteExchangeHandler(IRepository<ExchangeEntity> repository)
    {
        _repository = repository;
    }
    public async Task<DeleteExchangeResponse> Handle(DeleteExchangeRequest request, CancellationToken cancellationToken)
    {
        var delete = await _repository.RemoveAsync(request.ExchangeId);
        if (delete == 0)
        {
            return new DeleteExchangeResponse() { Success = false };
        }

        return new DeleteExchangeResponse() { Data = request.ExchangeId };
    }
}
