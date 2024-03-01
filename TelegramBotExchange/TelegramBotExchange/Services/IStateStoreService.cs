using TelegramBotExchange.Database.Models;
using TelegramBotExchange.Helpers.UserStates;

namespace TelegramBotExchange.Services;

public interface IStateStoreService
{
    public Task CreateState(long chatId, TypeOperationEnum typeOperation);
    public Task<UserEntity> GetUser(long chatId);
    public Task MoveState(long chatId, TypeOperationEnum typeOperation);
    public Task MovePage(long chatId, bool forward);
    public Task ResetPage(long chatId);
    public Task SetTypeList(long chatId, ListType listType);
    public Task SetItemId(long chatId, Guid id);
    public Task SaveDirectionOperationData(long chatId, TypeOperationEnum typeOperation, DirectionOperationEntity directionOperation);
    public Task SaveDirectionExchangeData(long chatId, TypeOperationEnum typeOperation, DirectionExchangeEntity directionExchange);
    public Task SaveExchangeData(long chatId, TypeOperationEnum typeOperation, ExchangeEntity exchangeEntity);
    public Task SaveOperationData(long chatId, TypeOperationEnum typeOperation, OperationEntity operationEntity);
    public Task<DirectionOperationEntity?> GetDirectionOperationData(long chatId);
    public Task<DirectionExchangeEntity?> GetDirectionExchangeData(long chatId);
    public Task<ExchangeEntity?> GetExchangeData(long chatId);
    public Task<OperationEntity?> GetOperationData(long chatId);
    public Task ResetItemId(long chatId, TypeOperationEnum typeOperation);
    public Task DeleteData(long chatId);
}
