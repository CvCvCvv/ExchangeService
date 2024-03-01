using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsOperation;

public class UpdateDirectionOperationRequest : IRequest<UpdateDirectionOperationResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid DirectionExchangeId { get; set; }
}
