using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Exchanges;

public class DeleteExchangeRequest : IRequest<DeleteExchangeResponse>
{
    public Guid ExchangeId { get; set; }
}
