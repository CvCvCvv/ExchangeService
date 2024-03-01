using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.Exchanges;

public class GetExchangeRequest : BaseRequest
{
    public GetExchangeRequest(Guid exchangeId)
    {
        ExchangeId = exchangeId;
    }

    public Guid ExchangeId { get; set; }

    internal override string EndPoint => $"exchanges/{ExchangeId}";

    internal override Method Method => Method.Get;
}
