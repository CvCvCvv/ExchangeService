using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsOperation;

public class GetDirectionOperationRequest : BaseRequest
{
    public GetDirectionOperationRequest(Guid directionOperationId)
    {
        DirectionOperationId = directionOperationId;
    }

    public Guid DirectionOperationId { get; set; }

    internal override string EndPoint => $"directionOperation/{DirectionOperationId}";

    internal override Method Method => Method.Get;
}
