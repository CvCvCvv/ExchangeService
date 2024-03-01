namespace ExchangeService.Application.Domains.Abstractions.Entities;

public class DirectionOperationEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<OperationEntity> Operations { get; set; } = new();
    public DirectionExchangeEntity DirectionExchange { get; set; } = null!;
    public Guid DirectionExchangeId { get; set; }
}
