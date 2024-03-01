using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsExchange;

public class GetDirectionExchangeRequest : BaseRequest
{
    public GetDirectionExchangeRequest(Guid directionExchangeId)
    {

        DirectionExchangeId = directionExchangeId;

    }

    public Guid DirectionExchangeId { get; set; }

    internal override string EndPoint => $"directionsExchange/{DirectionExchangeId}";

    internal override Method Method => Method.Get;
}
