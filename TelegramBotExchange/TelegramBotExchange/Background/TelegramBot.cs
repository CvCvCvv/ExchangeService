
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotExchange.Domains.Options;
using TelegramBotExchange.Helpers.Telegram;
using TelegramBotExchange.Services;

namespace TelegramBotExchange.Background;

public class TelegramBot : BackgroundService
{
    private readonly TelegramOptions _botOptions;
    private readonly IChatService _chatService;
    private readonly ITelegramBotClient _telegramBot;
    private readonly ReceiverOptions _receiverOptions;

    public TelegramBot(IOptions<TelegramOptions> botOptions, IChatService chatService)
    {
        _botOptions = botOptions.Value ?? throw new ArgumentException("botOptions exists!");

        _telegramBot = new TelegramBotClient(_botOptions.Token);

        _receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = new[]
        {
            UpdateType.Message,
            UpdateType.CallbackQuery
        },
            ThrowPendingUpdates = _botOptions.ThrowPendingUpdates
        };

        _chatService = chatService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramBot.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, stoppingToken);

        return Task.CompletedTask;
    }

    private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await _chatService.GetMessage(_telegramBot, update);
                    break;
                case UpdateType.CallbackQuery:
                    await _chatService.GetCallbackMessage(_telegramBot, update);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            await _telegramBot.SendTextMessageAsync(update.Message == null ? update.CallbackQuery!.Message!.Chat!.Id! : update.Message.Chat.Id, "Произошла ошибка!!", replyMarkup: ButtonsTelegram.MainMenu);
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }

    private async Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync(error.Message);

        await Task.CompletedTask;
    }
}
