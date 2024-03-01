using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using ExchangeService.Client.Requests.Enums;
using RestSharp;

namespace ExchangeService.Client.Requests.Operations;

public class CreateOperationRequest : BaseRequest
{
    public CreateOperationRequest(Guid directionOperationId, Guid exchangeId, decimal sum, decimal volume, OperationTypeEnum operationType)
    {
        DirectionOperationId = directionOperationId;
        ExchangeId = exchangeId;
        Sum = sum;
        Volume = volume;
        OperationType = operationType;
    }

    public Guid DirectionOperationId { get; set; }

    public Guid ExchangeId { get; set; }

    public decimal Sum { get; set; }

    public decimal Volume { get; set; }

    public OperationTypeEnum OperationType { get; set; }

    internal override string EndPoint => "operations/create";

    internal override Method Method => Method.Post;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddSimpleStruct("directionOperationId", DirectionOperationId);
            res.AddSimpleStruct("exchangeId", ExchangeId);
            res.AddDecimal("sum", Sum);
            res.AddDecimal("volume", Volume);
            res.AddEnum("operationType", OperationType);
            
            return res;
        }
    }
}
