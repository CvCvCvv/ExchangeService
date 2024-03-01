using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsOperation;

public class CreateDirectionOperationRequest : IRequest<CreateDirectionOperationResponse>
{
    public string Name { get; set; } = null!;
    public Guid DirectionExchangeId { get; set; }
}
