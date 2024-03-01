namespace ExchangeService.Application.Domains.Abstractions.Entities;

public class ExchangeEntity
{
    public Guid Id { get; set; }
    public DirectionExchangeEntity DirectionExchange { get; set; } = null!;
    public Guid DirectionExchangeId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public List<OperationEntity> Operations { get; set; } = new();
    public string NameExecutor { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public bool Closed { get; set; }
}
