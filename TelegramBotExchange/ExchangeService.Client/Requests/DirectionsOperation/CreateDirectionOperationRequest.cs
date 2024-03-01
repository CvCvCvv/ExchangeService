using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsOperation;

public class CreateDirectionOperationRequest : BaseRequest
{
    public CreateDirectionOperationRequest(string name, Guid directionExchangeId)
    {
        Name = name;
        DirectionExchangeId = directionExchangeId;
    }

    public string Name { get; set; }

    public Guid DirectionExchangeId { get; set; }

    internal override string EndPoint => "directionOperation/create";

    internal override Method Method => Method.Post;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.Add("name", Name);
            res.AddSimpleStruct("directionExchangeId", DirectionExchangeId);

            return res;
        }
    }
}
