using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsExchange;

public class CreateDirectionExchangeRequest : IRequest<CreateDirectionExchangeResponse>
{
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
}
