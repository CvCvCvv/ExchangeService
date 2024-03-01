namespace ExchangeService.Client.Abstractions.Models;

public class Exchange
{
    public Guid Id { get; set; }
    public Guid DirectionExchangeId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string NameExecutor { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public bool Closed { get; set; }
}
