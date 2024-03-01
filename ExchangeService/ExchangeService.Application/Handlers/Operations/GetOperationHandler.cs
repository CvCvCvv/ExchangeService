using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Operations;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Handlers.Operations;

public class GetOperationHandler : IRequestHandler<GetOperationRequest, GetOperationByIdResponse>
{
    private readonly IRepository<OperationEntity> _repository;

    public GetOperationHandler(IRepository<OperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<GetOperationByIdResponse> Handle(GetOperationRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FindByIdAsync(request.OperationId);
        if (item == null)
        {
            return new GetOperationByIdResponse() { Success = false };
        }

        return new GetOperationByIdResponse() { Data = item };
    }
}
