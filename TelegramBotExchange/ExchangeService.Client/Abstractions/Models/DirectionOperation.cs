namespace ExchangeService.Client.Abstractions.Models;

public class DirectionOperation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid DirectionExchangeId { get; set; }
}
