using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsOperation;

public class GetDirectionsOperationRequest : BaseRequest
{
    public GetDirectionsOperationRequest(string? name, Guid? directionExchangeId)
    {
        Name = name;
        DirectionExchangeId = directionExchangeId;
    }

    public string? Name { get; set; }

    public Guid? DirectionExchangeId { get; set; }

    internal override string EndPoint => "directionOperation";

    internal override Method Method => Method.Get;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();

            res.AddStringIfNotEmptyOrWhiteSpace("name", Name);
            res.AddSimpleStructIfNotNull("directionExchangeId", DirectionExchangeId);

            return res;
        }
    }
}
