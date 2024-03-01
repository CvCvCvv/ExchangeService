using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsExchange;

public class GetDirectionsExchangeRequest : IRequest<GetDirectionsExchangeResponse>
{
    public string? Name { get; set; }
    public DateTime? DateStart { get; set; }
}
