using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsExchange;

public class DeleteDirectionExchangeRequest : BaseRequest
{
    public DeleteDirectionExchangeRequest(Guid directionExchangeId)
    {

        DirectionExchangeId = directionExchangeId;

    }

    public Guid DirectionExchangeId { get; set; }

    internal override string EndPoint => $"directionsExchange/delete/{DirectionExchangeId}";

    internal override Method Method => Method.Delete;
}

