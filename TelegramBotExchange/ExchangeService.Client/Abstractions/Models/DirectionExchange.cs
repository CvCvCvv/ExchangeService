namespace ExchangeService.Client.Abstractions.Models;

public class DirectionExchange
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
}
