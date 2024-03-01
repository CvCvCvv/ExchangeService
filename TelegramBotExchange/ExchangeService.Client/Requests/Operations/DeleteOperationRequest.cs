using ExchangeService.Client.Common;
using RestSharp;

namespace ExchangeService.Client.Requests.Operations;

public class DeleteOperationRequest : BaseRequest
{
    public DeleteOperationRequest(Guid operationId)
    {
        OperationId = operationId;
    }

    public Guid OperationId { get; set; }

    internal override string EndPoint => $"operations/delete/{OperationId}";

    internal override Method Method => Method.Delete;
}
