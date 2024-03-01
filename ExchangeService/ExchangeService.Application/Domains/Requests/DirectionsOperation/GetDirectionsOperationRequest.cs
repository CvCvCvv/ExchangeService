using ExchangeService.Application.Domains.Responses.DirectionsOperation;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsOperation;

public class GetDirectionsOperationRequest : IRequest<GetDirectionsOperationResponse>
{
    public string? Name { get; set; }
}
