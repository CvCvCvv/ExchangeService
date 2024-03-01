using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExchangeService.Database.Repositories;

public class OperationsRepository : IRepository<OperationEntity>
{
    private readonly DbSet<OperationEntity> _dbSet;
    private readonly ExchangeServiceContext _context;
    
    public OperationsRepository(ExchangeServiceContext context)
    {

        _context = context;
        _dbSet = _context.Set<OperationEntity>();

    }

    public int Create(OperationEntity item)
    {
        _dbSet.Add(item);

        return _context.SaveChanges();
    }

    public async Task<int> CreateAsync(OperationEntity item)
    {
        _dbSet.Add(item);

        return await _context.SaveChangesAsync();
    }

    public OperationEntity FindById(Guid id)
    {
        return _dbSet.FirstOrDefault(a => a.Id == id)!;
    }

    public async Task<OperationEntity> FindByIdAsync(Guid id)
    {
        return (await _dbSet.FirstOrDefaultAsync(a => a.Id == id))!;
    }

    public IEnumerable<OperationEntity> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<OperationEntity> Get(Func<OperationEntity, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    public async Task<IEnumerable<OperationEntity>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IEnumerable<OperationEntity> GetWitchInclude(params Expression<Func<OperationEntity, object>>[] includeProperties)
    {
        return Include(includeProperties);
    }

    public IEnumerable<OperationEntity> GetWitchInclude(Func<OperationEntity, bool> predicate, params Expression<Func<OperationEntity, object>>[] includeProperties)
    {
        var query = Include(includeProperties);

        return query.Where(predicate).ToList();
    }

    public int Remove(Guid id)
    {
        var item = _dbSet.Find(id);
        if (item == null)
        {
            return 0;
        }
        _dbSet.Remove(item);

        return _context.SaveChanges();
    }

    public async Task<int> RemoveAsync(Guid id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item == null)
        {
            return 0;
        }
        _dbSet.Remove(item);

        return await _context.SaveChangesAsync();
    }

    public int Update(OperationEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(OperationEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    private IQueryable<OperationEntity> Include(params Expression<Func<OperationEntity, object>>[] includeProperties)
    {
        IQueryable<OperationEntity> query = _dbSet.AsNoTracking();

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
