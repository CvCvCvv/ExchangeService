using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Exchanges;

public class GetExchangesRequest : IRequest<GetExchangesResponse>
{
    public Guid? DirectionExchangeId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string? NameExecutor { get; set; }
    public string? Symbol { get; set; }
    public bool? Closed { get; set; }
}
