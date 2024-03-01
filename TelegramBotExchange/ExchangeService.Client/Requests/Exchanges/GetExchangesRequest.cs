using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.Exchanges;

public class GetExchangesRequest : BaseRequest
{
    public GetExchangesRequest(Guid? durectionExchangeId, DateTime? dateStart, DateTime? dateEnd, string? nameExecutor, string? symbol, bool? closed)
    {
        DirectionExchangeId = durectionExchangeId;
        DateStart = dateStart;
        DateEnd = dateEnd;
        NameExecutor = nameExecutor;
        Symbol = symbol;
        Closed = closed;
    }

    public Guid? DirectionExchangeId { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public string? NameExecutor { get; set; }

    public string? Symbol { get; set; }

    public bool? Closed { get; set; }

    internal override string EndPoint => "exchanges";

    internal override Method Method => Method.Get;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddSimpleStructIfNotNull("directionExchangeId", DirectionExchangeId);
            
            if(DateStart != null)
                res.AddDateTime("dateStart", DateStart);
            
            if(DateEnd != null)
                res.AddDateTime("dateEnd", DateEnd);
            
            res.AddStringIfNotEmptyOrWhiteSpace("nameExecutor", NameExecutor);
            
            if (Closed != null)
                res.Add("closed", Closed.ToString()!.ToLower());

            return res;
        }
    }
}
