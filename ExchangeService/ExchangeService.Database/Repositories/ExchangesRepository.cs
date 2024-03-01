using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExchangeService.Database.Repositories;

public class ExchangesRepository : IRepository<ExchangeEntity>
{
    private readonly DbSet<ExchangeEntity> _dbSet;
    private readonly ExchangeServiceContext _context;
    public ExchangesRepository(ExchangeServiceContext context)
    {

        _context = context;
        _dbSet = _context.Set<ExchangeEntity>();
    }

    public int Create(ExchangeEntity item)
    {
        _dbSet.Add(item);

        return _context.SaveChanges();
    }

    public async Task<int> CreateAsync(ExchangeEntity item)
    {
        _dbSet.Add(item);

        return await _context.SaveChangesAsync();
    }

    public ExchangeEntity FindById(Guid id)
    {
        return _dbSet.FirstOrDefault(a => a.Id == id)!;
    }

    public async Task<ExchangeEntity> FindByIdAsync(Guid id)
    {
        return (await _dbSet.FirstOrDefaultAsync(a => a.Id == id))!;
    }

    public IEnumerable<ExchangeEntity> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<ExchangeEntity> Get(Func<ExchangeEntity, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    public async Task<IEnumerable<ExchangeEntity>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IEnumerable<ExchangeEntity> GetWitchInclude(params Expression<Func<ExchangeEntity, object>>[] includeProperties)
    {
        return Include(includeProperties);
    }

    public IEnumerable<ExchangeEntity> GetWitchInclude(Func<ExchangeEntity, bool> predicate, params Expression<Func<ExchangeEntity, object>>[] includeProperties)
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

    public int Update(ExchangeEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(ExchangeEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    private IQueryable<ExchangeEntity> Include(params Expression<Func<ExchangeEntity, object>>[] includeProperties)
    {
        IQueryable<ExchangeEntity> query = _dbSet.AsNoTracking();

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
