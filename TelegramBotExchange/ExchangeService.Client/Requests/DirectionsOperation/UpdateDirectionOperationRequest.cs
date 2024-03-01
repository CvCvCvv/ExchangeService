using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsOperation;

public class UpdateDirectionOperationRequest : BaseRequest
{
    public UpdateDirectionOperationRequest(Guid id, string name, Guid directionExchangeId)
    {
        Id = id;
        Name = name;
        DirectionExchangeId = directionExchangeId;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid DirectionExchangeId { get; set; }

    internal override string EndPoint => "directionOperation/update";

    internal override Method Method => Method.Put;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddSimpleStruct("id", Id);
            res.Add("name", Name);
            res.AddSimpleStruct("directionExchangeId", DirectionExchangeId);

            return res;
        }
    }
}
