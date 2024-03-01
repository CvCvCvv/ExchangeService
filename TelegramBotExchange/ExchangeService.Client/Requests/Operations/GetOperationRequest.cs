using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.Operations;

public class GetOperationRequest : BaseRequest
{
    public GetOperationRequest(Guid operationId)
    {
        OperationId = operationId;
    }

    public Guid OperationId { get; set; }

    internal override string EndPoint => $"operations/{OperationId}";

    internal override Method Method => Method.Get;
}
