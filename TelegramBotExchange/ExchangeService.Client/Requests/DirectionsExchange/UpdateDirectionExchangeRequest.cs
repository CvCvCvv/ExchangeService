using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsExchange;

public class UpdateDirectionExchangeRequest : BaseRequest
{
    public UpdateDirectionExchangeRequest(Guid id, string name, DateTime dateStart)
    {
        Id = id;
        Name = name;
        DateStart = dateStart;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime DateStart { get; set; }

    internal override string EndPoint => "directionsExchange/update";

    internal override Method Method => Method.Put;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddSimpleStruct("id", Id);
            res.Add("name", Name);
            res.AddDateTime("dateStart", DateStart);

            return res;
        }
    }
}
