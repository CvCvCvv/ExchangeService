using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsOperation;
using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsOperation;

public class GetDirectionOperationHandler : IRequestHandler<GetDirectionOperationRequest, GetDirectionOperationByIdResponse>
{
    private readonly IRepository<DirectionOperationEntity> _repository;

    public GetDirectionOperationHandler(IRepository<DirectionOperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetDirectionOperationByIdResponse> Handle(GetDirectionOperationRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FindByIdAsync(request.DirectionOperationId);
        if (item == null)
        {
            return new GetDirectionOperationByIdResponse() { Success = false };
        }

        return new GetDirectionOperationByIdResponse() { Data = item };
    }
}
