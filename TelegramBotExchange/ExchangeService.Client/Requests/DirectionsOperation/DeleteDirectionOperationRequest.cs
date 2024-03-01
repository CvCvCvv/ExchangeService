using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.DirectionsOperation;

public class DeleteDirectionOperationRequest : BaseRequest
{
    public DeleteDirectionOperationRequest(Guid directionOperationId)
    {
        DirectionOperationId = directionOperationId;
    }

    public Guid DirectionOperationId { get; set; }

    internal override string EndPoint => $"directionOperation/delete/{DirectionOperationId}";

    internal override Method Method => Method.Delete;
}
