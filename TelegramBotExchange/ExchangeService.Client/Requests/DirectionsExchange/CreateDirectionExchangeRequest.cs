using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsExchange;

public class CreateDirectionExchangeRequest : BaseRequest
{
    public CreateDirectionExchangeRequest(string name, DateTime dateStart)
    {
        Name = name;
        DateStart = dateStart;
    }

    public string Name { get; set; }

    public DateTime DateStart { get; set; }

    internal override string EndPoint => "directionsExchange/create";

    internal override Method Method => Method.Post;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.Add("name", Name);
            res.AddDateTime("dateStart", DateStart);

            return res;
        }
    }
}
