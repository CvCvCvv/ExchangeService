namespace TelegramBotExchange.Domains.Options;

public class TelegramOptions
{
    public string Token { get; set; } = null!;
    public bool ThrowPendingUpdates { get; set; }
}
