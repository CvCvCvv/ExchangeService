using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.Exchanges;

public class UpdateExchangeRequest : BaseRequest
{
    public UpdateExchangeRequest(Guid id, Guid durectionExchangeId, DateTime dateStart, DateTime dateEnd, string nameExecutor, string symbol, bool closed)
    {
        Id = id;
        DirectionExchangeId = durectionExchangeId;
        DateStart = dateStart;
        DateEnd = dateEnd;
        NameExecutor = nameExecutor;
        Symbol = symbol;
        Closed = closed;
    }

    public Guid Id { get; set; }

    public Guid DirectionExchangeId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public string NameExecutor { get; set; }

    public string Symbol { get; set; }

    public bool Closed { get; set; }

    internal override string EndPoint => "exchanges/update";

    internal override Method Method => Method.Put;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddSimpleStruct("id", Id);
            res.AddSimpleStruct("directionExchangeId", DirectionExchangeId);
            res.AddDateTime("dateStart", DateStart);
            res.AddDateTime("dateEnd", DateEnd);
            res.Add("symbol", Symbol);
            res.Add("nameExecutor", NameExecutor);
            res.Add("closed", Closed.ToString().ToLower());

            return res;
        }
    }
}
