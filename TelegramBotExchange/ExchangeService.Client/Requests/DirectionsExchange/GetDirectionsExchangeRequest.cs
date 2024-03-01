using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsExchange;

public class GetDirectionsExchangeRequest : BaseRequest
{
    public GetDirectionsExchangeRequest(string? name, DateTime? dateStart)
    {
        Name = name;
        DateStart = dateStart;
    }

    public string? Name { get; set; }

    public DateTime? DateStart { get; set; }

    internal override string EndPoint => "directionsExchange";

    internal override Method Method => Method.Get;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddStringIfNotEmptyOrWhiteSpace("name", Name);
            if(DateStart != null)
                res.AddDateTime("dateStart", DateStart);

            return res;
        }
    }
}
