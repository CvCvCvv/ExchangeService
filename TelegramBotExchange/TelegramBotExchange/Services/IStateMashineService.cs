using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotExchange.Services;

public interface IStateMashineService
{
    void MovingState(IStateStoreService stateStore, ITelegramBotClient telegramBotClient, Update update);
}
