using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.Operations;
using ExchangeService.Application.Domains.Responses.Operations;
using MediatR;

namespace ExchangeService.Application.Handlers.Operations;

public class DeleteOperationHandler : IRequestHandler<DeleteOperationRequest, DeleteOperationResponse>
{
    private readonly IRepository<OperationEntity> _repository;

    public DeleteOperationHandler(IRepository<OperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<DeleteOperationResponse> Handle(DeleteOperationRequest request, CancellationToken cancellationToken)
    {
        var delete = await _repository.RemoveAsync(request.OperationId);
        if (delete == 0)
        {
            return new DeleteOperationResponse() { Success = false };
        }

        return new DeleteOperationResponse() { Data = request.OperationId };
    }
}
