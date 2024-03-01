using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Exchanges;

public class GetExchangeRequest : IRequest<GetExchangeByIdResponse>
{
    public Guid ExchangeId { get; set; }
}
