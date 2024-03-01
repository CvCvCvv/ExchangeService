using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.Exchanges;

public class CreateExchangeRequest : BaseRequest
{
    public CreateExchangeRequest(Guid directionExchangeId, DateTime dateStart, DateTime dateEnd, string nameExecutor, string symbol, bool closed)
    {
        DirectionExchangeId = directionExchangeId;
        DateStart = dateStart;
        DateEnd = dateEnd;
        NameExecutor = nameExecutor;
        Symbol = symbol;
        Closed = closed;
    }

    public Guid DirectionExchangeId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public string NameExecutor { get; set; }

    public string Symbol { get; set; }

    public bool Closed { get; set; }

    internal override string EndPoint => "exchanges/create";

    internal override Method Method => Method.Post;

    internal override IDictionary<string, string> Properties {
        get { 
            var res = new Dictionary<string, string>();

            res.AddSimpleStruct("directionExchangeId", DirectionExchangeId);
            res.AddDateTime("dateStart", DateStart);
            res.AddDateTime("dateEnd", DateEnd);
            res.Add("nameExecutor", NameExecutor);
            res.Add("symbol", Symbol);
            res.Add("closed", Closed.ToString().ToLower());

            return res;
        }
    }
}
