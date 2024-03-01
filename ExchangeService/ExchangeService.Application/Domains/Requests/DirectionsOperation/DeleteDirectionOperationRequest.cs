using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsOperation;

public class DeleteDirectionOperationRequest : IRequest<DeleteDirectionOperationResponse>
{
    public Guid DirectionOperationId { get; set; }
}
