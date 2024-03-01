using Microsoft.EntityFrameworkCore;
using TelegramBotExchange.Database;
using TelegramBotExchange.Database.Models;
using TelegramBotExchange.Helpers.UserStates;

namespace TelegramBotExchange.Services
{
    public class StateStoreService : IStateStoreService
    {
        private readonly ApplicationDbContext _context;

        public StateStoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateState(long chatId, TypeOperationEnum typeOperation)
        {
            var userDb = _context.Users.FirstOrDefault(a => a.ChatId == chatId);
            if (userDb != null)
            {
                userDb.TypeOperation = typeOperation;
                userDb.State = 1;
                userDb.ListType = ListType.None;

                _context.Users.Update(userDb);
                await _context.SaveChangesAsync();
                return;
            }

            var user = new UserEntity() { ChatId = chatId, TypeOperation = typeOperation, State = 1, ListType = ListType.None };
            var exchange = new ExchangeEntity() { User = user };
            var exchangeDirection = new DirectionExchangeEntity() { User = user };
            var operation = new OperationEntity() { User = user };
            var operationDirection = new DirectionOperationEntity() { User = user };

            _context.Users.Add(user);
            _context.Exchanges.Add(exchange);
            _context.DirectionsExchange.Add(exchangeDirection);
            _context.Operations.Add(operation);
            _context.DirectionsOperation.Add(operationDirection);

            await _context.SaveChangesAsync();


        }

        public async Task DeleteData(long chatId)
        {
            var user = _context.Users.FirstOrDefault(a => a.ChatId == chatId);
            if (user is null)
                return;

            var exchange = _context.Exchanges.Where(a => a.UserId == user.Id);
            if (exchange is not null)
                _context.Exchanges.RemoveRange(exchange);

            var directionExchange = _context.DirectionsExchange.Where(a => a.UserId == user.Id);
            if (directionExchange is not null)
                _context.DirectionsExchange.RemoveRange(directionExchange);

            var directionOperation = _context.DirectionsOperation.Where(a => a.UserId == user.Id);
            if (directionOperation is not null)
                _context.DirectionsOperation.RemoveRange(directionOperation);

            var operation = _context.Operations.Where(a => a.UserId == user.Id);
            if (operation is not null)
                _context.Operations.RemoveRange(operation);


            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<DirectionExchangeEntity?> GetDirectionExchangeData(long chatId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null)
                return null;

            user.State = 1;
            user.TypeOperation = TypeOperationEnum.None;
            await _context.SaveChangesAsync();

            return await _context.DirectionsExchange.FirstOrDefaultAsync(a => a.UserId == user.Id);
        }

        public async Task<DirectionOperationEntity?> GetDirectionOperationData(long chatId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null)
                return null;

            user.State = 1;
            user.TypeOperation = TypeOperationEnum.None;
            await _context.SaveChangesAsync();

            return await _context.DirectionsOperation.FirstOrDefaultAsync(a => a.UserId == user.Id);
        }

        public async Task<ExchangeEntity?> GetExchangeData(long chatId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null)
                return null;

            user.State = 1;
            user.TypeOperation = TypeOperationEnum.None;
            await _context.SaveChangesAsync();

            return await _context.Exchanges.FirstOrDefaultAsync(a => a.UserId == user.Id);
        }

        public async Task<OperationEntity?> GetOperationData(long chatId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null)
                return null;

            user.State = 1;
            user.TypeOperation = TypeOperationEnum.None;
            await _context.SaveChangesAsync();

            return await _context.Operations.FirstOrDefaultAsync(a => a.UserId == user.Id);
        }

        public async Task<UserEntity> GetUser(long chatId)
        {
            return (await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId))!;
        }

        public async Task MovePage(long chatId, bool forward)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);

            if (user == null)
                return;

            if (!forward && user.Page != 0)
                user.Page--;
            else
                user.Page++;
        }

        public async Task MoveState(long chatId, TypeOperationEnum typeOperation)
        {
            var user = _context.Users.FirstOrDefault(a => a.ChatId == chatId && a.TypeOperation == typeOperation);

            if (user is null)
                return;

            user.State++;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task ResetPage(long chatId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);

            if (user == null)
                return;

            user.Page = 0;

            await _context.SaveChangesAsync();
        }

        public async Task SaveDirectionExchangeData(long chatId, TypeOperationEnum typeOperation, DirectionExchangeEntity directionExchange)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null) return;

            var directExchange = await _context.DirectionsExchange.FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (directExchange is null) return;

            directExchange.ItemId = directionExchange.ItemId == null ? directExchange.ItemId : directionExchange.ItemId;
            directExchange.Name = directionExchange.Name == null ? directExchange.Name : directionExchange.Name;
            directExchange.DateStart = directionExchange.DateStart == null ? directExchange.DateStart : directionExchange.DateStart;
        }

        public async Task SaveDirectionOperationData(long chatId, TypeOperationEnum typeOperation, DirectionOperationEntity directionOperation)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null) return;

            var directOperation = await _context.DirectionsOperation.FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (directOperation is null) return;

            directOperation.ItemId = directionOperation.ItemId != null ? directionOperation.ItemId : directOperation.ItemId;
            directOperation.Name = directionOperation.Name == null ? directOperation.Name : directionOperation.Name;
            directOperation.DirectionExchangeId = directionOperation.DirectionExchangeId == null ? directOperation.DirectionExchangeId : directionOperation.DirectionExchangeId;
        }

        public async Task SaveExchangeData(long chatId, TypeOperationEnum typeOperation, ExchangeEntity exchangeEntity)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null) return;

            var exchange = await _context.Exchanges.FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (exchange is null) return;

            exchange.ItemId = exchangeEntity.ItemId != null ? exchangeEntity.ItemId : exchange.ItemId;
            exchange.NameExecutor = exchangeEntity.NameExecutor == null ? exchange.NameExecutor : exchangeEntity.NameExecutor;
            exchange.Symbol = exchangeEntity.Symbol == null ? exchange.Symbol : exchangeEntity.Symbol;
            exchange.DateEnd = exchangeEntity.DateEnd == null ? exchange.DateEnd : exchangeEntity.DateEnd;
            exchange.DateStart = exchangeEntity.DateStart == null ? exchange.DateStart : exchangeEntity.DateStart;
            exchange.Closed = exchangeEntity.Closed == null ? exchange.Closed : exchangeEntity.Closed;
            exchange.DirectionExchangeId = exchangeEntity.DirectionExchangeId == null ? exchange.DirectionExchangeId : exchangeEntity.DirectionExchangeId;
        }

        public async Task SaveOperationData(long chatId, TypeOperationEnum typeOperation, OperationEntity operationEntity)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null) return;

            var operation = await _context.Operations.FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (operation is null) return;

            operation.ItemId = operationEntity.ItemId != null ? operationEntity.ItemId : operation.ItemId;
            operation.Volume = operationEntity.Volume == null ? operation.Volume : operationEntity.Volume;
            operation.Sum = operationEntity.Sum == null ? operation.Sum : operationEntity.Sum;
            operation.OperationType = operationEntity.OperationType == null ? operation.OperationType : operationEntity.OperationType;
            operation.DirectionOperationId = operationEntity.DirectionOperationId == null ? operation.DirectionOperationId : operationEntity.DirectionOperationId;
            operation.ExchangeId = operationEntity.ExchangeId == null ? operation.ExchangeId : operationEntity.ExchangeId;
        }

        public async Task SetItemId(long chatId, Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);
            if (user is null) return;

            user.ItemId = id;

            await _context.SaveChangesAsync();
        }

        public async Task SetTypeList(long chatId, ListType listType)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.ChatId == chatId);

            if (user is null)
                return;

            user.ListType = listType;

            await _context.SaveChangesAsync();
        }

        public async  Task ResetItemId(long chatId, TypeOperationEnum typeOperation)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a=>a.ChatId == chatId);

            var exchange = await _context.Exchanges.FirstOrDefaultAsync(a=>a.UserId == user!.Id);
            var operation = await _context.Operations.FirstOrDefaultAsync(a=>a.UserId == user!.Id);
            var directionOperation = await _context.DirectionsOperation.FirstOrDefaultAsync(a => a.UserId == user!.Id);
            var directionExchange = await _context.DirectionsExchange.FirstOrDefaultAsync(a => a.UserId == user!.Id);

            switch (typeOperation)
            {
                case TypeOperationEnum.AddExchange:
                    exchange!.ItemId = null;
                    break;
                case TypeOperationEnum.AddOperation:
                    operation!.ItemId = null;
                    break;
                case TypeOperationEnum.AddDirectionExchange:
                    directionExchange!.ItemId = null;
                    break;
                case TypeOperationEnum.AddDirectionOperation:
                    directionOperation!.ItemId = null;
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
