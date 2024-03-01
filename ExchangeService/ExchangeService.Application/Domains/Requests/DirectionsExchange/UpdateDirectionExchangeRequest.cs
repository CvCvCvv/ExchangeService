using ExchangeService.Application.Domains.Responses.DirectionsExchange;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.DirectionsExchange;

public class UpdateDirectionExchangeRequest : IRequest<UpdateDirectionExchangeResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
}
