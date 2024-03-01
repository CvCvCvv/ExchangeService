using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotExchange.Services;

public interface IChatService
{
    Task GetMessage(ITelegramBotClient telegramBot, Update message);
    Task GetCallbackMessage(ITelegramBotClient telegramBot, Update message);
}
