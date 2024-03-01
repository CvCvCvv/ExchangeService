using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.Exchanges;

public class DeleteExchangeRequest : BaseRequest
{
    public DeleteExchangeRequest(Guid exchangeId)
    {
        ExchangeId = exchangeId;
    }

    public Guid ExchangeId { get; set; }

    internal override string EndPoint => $"exchanges/delete/{ExchangeId}";

    internal override Method Method => Method.Delete;
}
