using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Application.Domains.Requests.DirectionsOperation;
using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Handlers.DirectionsOperation;

public class DeleteDirectionOperationHandler : IRequestHandler<DeleteDirectionOperationRequest, DeleteDirectionOperationResponse>
{
    private readonly IRepository<DirectionOperationEntity> _repository;

    public DeleteDirectionOperationHandler(IRepository<DirectionOperationEntity> repository)
    {
        _repository = repository;
    }

    public async Task<DeleteDirectionOperationResponse> Handle(DeleteDirectionOperationRequest request, CancellationToken cancellationToken)
    {
        var delete = await _repository.RemoveAsync(request.DirectionOperationId);
        if (delete == 0)
        {
            return new DeleteDirectionOperationResponse() { Success = false };
        }

        return new DeleteDirectionOperationResponse() { Data = request.DirectionOperationId };
    }
}
