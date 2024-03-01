using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsOperation;

public class GetDirectionOperationRequest : IRequest<GetDirectionOperationByIdResponse>
{
    public Guid DirectionOperationId { get; set; }
}
