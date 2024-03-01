using ExchangeService.Client.Common;
using ExchangeService.Client.Extensions;
using ExchangeService.Client.Requests.Enums;
using RestSharp;

namespace ExchangeService.Client.Requests.Operations;

public class GetOperationsRequest : BaseRequest
{
    public GetOperationsRequest(Guid? directionOperationId, Guid? exchangeId, decimal? sum, decimal? volume, OperationTypeEnum? operationType)
    {
        DirectionOperationId = directionOperationId;
        ExchangeId = exchangeId;
        Sum = sum;
        Volume = volume;
        OperationType = operationType;
    }

    public Guid? DirectionOperationId { get; set; }

    public Guid? ExchangeId { get; set; }

    public decimal? Sum { get; set; }

    public decimal? Volume { get; set; }

    public OperationTypeEnum? OperationType { get; set; }

    internal override string EndPoint => "operations";

    internal override Method Method => Method.Get;

    internal override IDictionary<string, string> Properties
    {
        get
        {
            var res = new Dictionary<string, string>();
            res.AddSimpleStructIfNotNull("DirectionOperationId", DirectionOperationId);
            res.AddDecimalIfNotNull("sum", Sum);
            res.AddDecimalIfNotNull("volume", Volume);

            if (OperationType != null)
                res.Add("operationType", OperationType.ToString()!);

            return res;
        }
    }
}
