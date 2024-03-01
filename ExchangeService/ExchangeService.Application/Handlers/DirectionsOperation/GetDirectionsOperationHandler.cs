using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsOperation;
using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsOperation;

public class GetDirectionsOperationHandler : IRequestHandler<GetDirectionsOperationRequest, GetDirectionsOperationResponse>
{
    private readonly IRepository<DirectionOperationEntity> _repository;

    public GetDirectionsOperationHandler(IRepository<DirectionOperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetDirectionsOperationResponse> Handle(GetDirectionsOperationRequest request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAsync();

        if (!items.Any())
        {
            return new GetDirectionsOperationResponse();
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            items = items.Where(w => w.Name == request.Name);
        }

        return new GetDirectionsOperationResponse() { Data = items.ToArray() };
    }
}
