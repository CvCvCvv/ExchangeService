namespace TelegramBotExchange.Database.Models;

public class ExchangeEntity
{
    public Guid Id { get; set; }
    public Guid? ItemId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int UserId { get; set; }
    public Guid? DirectionExchangeId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string? NameExecutor { get; set; }
    public string? Symbol { get; set; }
    public bool? Closed { get; set; }
}
