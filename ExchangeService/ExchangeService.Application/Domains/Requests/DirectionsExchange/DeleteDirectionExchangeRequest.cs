using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsExchange;

public class DeleteDirectionExchangeRequest : IRequest<DeleteDirectionExchangeResponse>
{
    public Guid DirectionExchangeId { get; set; }
}
