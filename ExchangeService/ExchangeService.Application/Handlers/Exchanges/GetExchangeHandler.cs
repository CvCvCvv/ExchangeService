using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Exchanges;
using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Handlers.Exchanges;

public class GetExchangeHandler : IRequestHandler<GetExchangeRequest, GetExchangeByIdResponse>
{
    private readonly IRepository<ExchangeEntity> _repository;

    public GetExchangeHandler(IRepository<ExchangeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetExchangeByIdResponse> Handle(GetExchangeRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FindByIdAsync(request.ExchangeId);
        if (item == null)
        {
            return new GetExchangeByIdResponse() { Success = false };
        }

        return new GetExchangeByIdResponse() { Data = item };
    }
}
