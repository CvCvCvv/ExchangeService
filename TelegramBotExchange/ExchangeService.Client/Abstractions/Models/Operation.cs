using ExchangeService.Client.Requests.Enums;

namespace ExchangeService.Client.Abstractions.Models;

public class Operation
{
    public Guid Id { get; set; }
    public Guid DirectionOperationId { get; set; }
    public Guid ExchangeId { get; set; }
    public decimal Sum { get; set; }
    public decimal Volume { get; set; }
    public OperationTypeEnum OperationType { get; set; }
}
