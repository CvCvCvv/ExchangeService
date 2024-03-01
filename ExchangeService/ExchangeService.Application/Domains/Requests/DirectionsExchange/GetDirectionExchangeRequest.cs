using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsExchange;

public class GetDirectionExchangeRequest : IRequest<GetDirectionExchangeByIdResponse>
{
    public Guid DirectionExchangeId { get; set; }
}
