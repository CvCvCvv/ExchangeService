using System.Linq.Expressions;

namespace ExchangeService.Application.Domains.Abstractions.Interfaces;

public interface IRepository<BaseEntity>
{
    int Create(BaseEntity item);
    Task<int> CreateAsync(BaseEntity item);
    BaseEntity FindById(Guid id);
    Task<BaseEntity> FindByIdAsync(Guid id);
    IEnumerable<BaseEntity> Get();
    Task<IEnumerable<BaseEntity>> GetAsync();
    IEnumerable<BaseEntity> Get(Func<BaseEntity, bool> predicate);
    int Remove(Guid id);
    Task<int> RemoveAsync(Guid id);
    int Update(BaseEntity item);
    Task<int> UpdateAsync(BaseEntity item);
    public IEnumerable<BaseEntity> GetWitchInclude(params Expression<Func<BaseEntity, object>>[] includeProperties);
    IEnumerable<BaseEntity> GetWitchInclude(Func<BaseEntity, bool> predicate, params Expression<Func<BaseEntity, object>>[] includeProperties);
}
