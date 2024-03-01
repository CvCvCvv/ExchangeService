using ExchangeService.Client.Client;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotExchange.Database.Models;
using TelegramBotExchange.Helpers.Telegram;
using TelegramBotExchange.Helpers.UserStates;

namespace TelegramBotExchange.Services;

public class StateMashineService : IStateMashineService
{
    private readonly string _baseUrl;
    private readonly int _skipElement;
    private readonly ExchangeServiceClient _exchangeServiceClient;

    public StateMashineService(IConfiguration configuration)
    {
        _baseUrl = configuration.GetValue<string>("ExchangeService:Url")!;
        _skipElement = configuration.GetValue<int>("ExchangeService:SkipElement");
        _exchangeServiceClient = new ExchangeServiceClient(_baseUrl);
    }
    public async void MovingState(IStateStoreService stateStore, ITelegramBotClient telegramBotClient, Update update)
    {
        var user = await stateStore.GetUser(update.Message!.Chat!.Id);

        switch (user.TypeOperation)
        {
            case TypeOperationEnum.AddExchange:
                await DiaologAddExchange(stateStore, telegramBotClient, update.Message!.Text!, update.Message.Chat.Id);
                break;
            case TypeOperationEnum.EditExchange:
                break;
            case TypeOperationEnum.AddOperation:
                await DiaologAddOperation(stateStore, telegramBotClient, update.Message!.Text!, update.Message.Chat.Id);
                break;
            case TypeOperationEnum.EditOperation:
                break;
            case TypeOperationEnum.AddDirectionExchange:
                await DiaologAddDirectionExchange(stateStore, telegramBotClient, update.Message!.Text!, update.Message.Chat.Id);
                break;
            case TypeOperationEnum.EditDirectiontExchange:
                break;
            case TypeOperationEnum.AddDirectionOperation:
                await DiaologAddDirectionOperation(stateStore, telegramBotClient, update.Message!.Text!, update.Message.Chat.Id);
                break;
            case TypeOperationEnum.EditDirectiontOperation:
                break;
            default:
                break;
        }
    }

    #region [DialogStep]

    private async Task DiaologAddDirectionOperation(IStateStoreService stateStore, ITelegramBotClient telegramBotClient, string message, ChatId chatId)
    {
        var user = await stateStore.GetUser((long)chatId.Identifier!);
        switch (user.State)
        {
            case 1:
                if (message.Length > 40)
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Слишком большое значение");
                }
                else
                {
                    await stateStore.SaveDirectionOperationData((long)chatId.Identifier, TypeOperationEnum.AddDirectionOperation, new DirectionOperationEntity() { Name = message });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddDirectionOperation);

                    await stateStore.ResetPage((long)chatId.Identifier);
                    await stateStore.SetTypeList((long)chatId.Identifier, ListType.DirectionsExchange);
                    await telegramBotClient.SendTextMessageAsync(chatId, "Выберите направление обмена:");
                    await SelectDirectionOperation(telegramBotClient, chatId, user);
                }
                break;
            default:
                break;
        }
    }

    private async Task DiaologAddDirectionExchange(IStateStoreService stateStore, ITelegramBotClient telegramBotClient, string message, ChatId chatId)
    {
        var user = await stateStore.GetUser((long)chatId.Identifier!);
        switch (user.State)
        {
            case 1:
                if (message.Length > 40)
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Слишком большое значение");
                }
                else
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Введите дату в формате ГГГГ-ММ-ДД");
                    await stateStore.SaveDirectionExchangeData((long)chatId.Identifier, TypeOperationEnum.AddDirectionOperation, new DirectionExchangeEntity() { Name = message });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddDirectionExchange);
                }
                break;
            case 2:
                if (DateTime.TryParse(message, out DateTime dateStart))
                {
                    await stateStore.SaveDirectionExchangeData((long)chatId.Identifier, TypeOperationEnum.AddDirectionOperation, new DirectionExchangeEntity() { DateStart = dateStart });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddDirectionExchange);

                    var exchangeCreate = await stateStore.GetDirectionExchangeData((long)chatId.Identifier);
                    if (exchangeCreate == null)
                        return;

                    if (exchangeCreate.ItemId == null)
                    {
                        var result = await _exchangeServiceClient.DirectionsExchange.CreateDirectionExchange(exchangeCreate.Name!, exchangeCreate.DateStart == null ? DateTime.Now : (DateTime)exchangeCreate.DateStart);
                        if (result.Success)
                            await telegramBotClient.SendTextMessageAsync(chatId, "Сохранено!", replyMarkup: ButtonsTelegram.MainMenu);
                        else
                            await telegramBotClient.SendTextMessageAsync(chatId, "Произошла ошибка!", replyMarkup: ButtonsTelegram.MainMenu);
                    }
                    else
                    {
                        var result = await _exchangeServiceClient.DirectionsExchange.UpdateDirectionExchange((Guid)exchangeCreate.ItemId, exchangeCreate.Name!, exchangeCreate.DateStart == null ? DateTime.Now : (DateTime)exchangeCreate.DateStart);
                        if (result.Success)
                            await telegramBotClient.SendTextMessageAsync(chatId, "Сохранено!", replyMarkup: ButtonsTelegram.MainMenu);
                        else
                            await telegramBotClient.SendTextMessageAsync(chatId, "Произошла ошибка!", replyMarkup: ButtonsTelegram.MainMenu);
                    }
                }
                else
                    await telegramBotClient.SendTextMessageAsync(chatId, "Неверно введена дата, повторите попытку");
                break;
            default:
                break;
        }
    }

    private async Task DiaologAddExchange(IStateStoreService stateStore, ITelegramBotClient telegramBotClient, string message, ChatId chatId)
    {
        var user = await stateStore.GetUser((long)chatId.Identifier!);
        switch (user.State)
        {
            case 1:
                if (message.Length > 40)
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Слишком большое значение");
                }
                else
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Введите название торговой пары");
                    await stateStore.SaveExchangeData((long)chatId.Identifier, TypeOperationEnum.AddExchange, new ExchangeEntity() { NameExecutor = message });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddExchange);
                }
                break;

            case 2:
                if (message.Length > 40)
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Слишком большое значение");
                }
                else
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Обмен закрыт? (да/нет)", replyMarkup: ButtonsTelegram.YesNoMenu);
                    await stateStore.SaveExchangeData((long)chatId.Identifier, TypeOperationEnum.AddExchange, new ExchangeEntity() { Symbol = message });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddExchange);
                }
                break;

            case 4:
                if (DateTime.TryParse(message, out DateTime dateStart))
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Дата окончания  в формате ГГГГ-ММ-ДД:");
                    await stateStore.SaveExchangeData((long)chatId.Identifier, TypeOperationEnum.AddExchange, new ExchangeEntity() { DateStart = dateStart });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddExchange);
                }
                else
                    await telegramBotClient.SendTextMessageAsync(chatId, "Неверно введена дата, повторите попытку");
                break;

            case 5:
                if (DateTime.TryParse(message, out DateTime dateEnd))
                {
                    await stateStore.SaveExchangeData((long)chatId.Identifier, TypeOperationEnum.AddExchange, new ExchangeEntity() { DateEnd = dateEnd });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddExchange);

                    await stateStore.ResetPage((long)chatId.Identifier);
                    await stateStore.SetTypeList((long)chatId.Identifier, ListType.DirectionsExchange);
                    await telegramBotClient.SendTextMessageAsync(chatId, "Выберите направление обмена:");
                    await SelectDirectionOperation(telegramBotClient, chatId, user);
                }
                else
                    await telegramBotClient.SendTextMessageAsync(chatId, "Неверно введена дата, повторите попытку");
                break;

            default:
                break;
        }
    }

    private async Task DiaologAddOperation(IStateStoreService stateStore, ITelegramBotClient telegramBotClient, string message, ChatId chatId)
    {
        var user = await stateStore.GetUser((long)chatId.Identifier!);
        switch (user.State)
        {
            case 1:
                if (decimal.TryParse(message, out decimal sum))
                {
                    await telegramBotClient.SendTextMessageAsync(chatId, "Введите уровень:");
                    await stateStore.SaveOperationData((long)chatId.Identifier, TypeOperationEnum.AddOperation, new OperationEntity() { Sum = sum });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddOperation);
                }
                else
                    await telegramBotClient.SendTextMessageAsync(chatId, "Неверно введено значение, повторите попытку");
                break;
            case 2:
                if (decimal.TryParse(message, out decimal volume))
                {
                    await stateStore.SaveOperationData((long)chatId.Identifier, TypeOperationEnum.AddOperation, new OperationEntity() { Volume = volume });
                    await stateStore.MoveState((long)chatId.Identifier, TypeOperationEnum.AddOperation);

                    await telegramBotClient.SendTextMessageAsync(chatId, "Выберите тип операции", replyMarkup: ButtonsTelegram.TypeOperationMenu);
                }
                else
                    await telegramBotClient.SendTextMessageAsync(chatId, "Неверно введено значение, повторите попытку");
                break;
            default:
                break;
        }
    }

    private async Task SelectDirectionOperation(ITelegramBotClient telegramBotClient, ChatId chatId, UserEntity user)
    {
        var client = new ExchangeServiceClient(_baseUrl);
        var result = await client.DirectionsExchange.GetDirectionsExchange(null, null);
        var otv = new StringBuilder("<i>Все направления обмена:</i>\n");

        if (result.Data == null)
        {
            await telegramBotClient.SendTextMessageAsync(chatId, "Выбор элемента или просмотр истории невозможен, список пуст", replyMarkup: ButtonsTelegram.GetMoveButtons(0, 0, new List<InlineKeyboardButton>()));
            return;
        }

        List<InlineKeyboardButton> buttons = new();

        var list = result.Data.Skip(user.Page * _skipElement).Take(_skipElement).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            otv.Append(MessageHelper.GetDirectionExchangeInfo(list[i]));
            buttons.Add(new InlineKeyboardButton((i + 1).ToString()) { CallbackData = list[i].Id.ToString() });
        }

        await telegramBotClient.SendTextMessageAsync(chatId, otv.ToString(), replyMarkup: ButtonsTelegram.GetMoveButtons(user.Page, list.Count, buttons, _skipElement), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    #endregion
}
