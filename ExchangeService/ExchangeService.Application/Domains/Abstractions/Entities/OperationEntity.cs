using ExchangeService.Application.Domains.Abstractions.Entities.Enums;

namespace ExchangeService.Application.Domains.Abstractions.Entities;

public class OperationEntity
{
    public Guid Id { get; set; }
    public DirectionOperationEntity DirectionOperation { get; set; } = null!;
    public Guid DirectionOperationId { get; set; }
    public ExchangeEntity Exchange { get; set; } = null!;
    public Guid ExchangeId { get; set; }
    public decimal Sum { get; set; }
    public decimal Volume { get; set; }
    public OperationType OperationType { get; set; }
}
