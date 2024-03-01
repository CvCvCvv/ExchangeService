using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExchangeService.Database.Repositories;

public class DirectionsExchangeRepository : IRepository<DirectionExchangeEntity>
{
    private readonly DbSet<DirectionExchangeEntity> _dbSet;
    private readonly ExchangeServiceContext _context;

    public DirectionsExchangeRepository(ExchangeServiceContext context)
    {
        _context = context;
        _dbSet = _context.Set<DirectionExchangeEntity>();
    }

    public int Create(DirectionExchangeEntity item)
    {
        _dbSet.Add(item);

        return _context.SaveChanges();
    }

    public async Task<int> CreateAsync(DirectionExchangeEntity item)
    {
        _dbSet.Add(item);

        return await _context.SaveChangesAsync();
    }

    public DirectionExchangeEntity FindById(Guid id)
    {
        return _dbSet.FirstOrDefault(a => a.Id == id)!;
    }

    public async Task<DirectionExchangeEntity> FindByIdAsync(Guid id)
    {
        return (await _dbSet.FirstOrDefaultAsync(a => a.Id == id))!;
    }

    public IEnumerable<DirectionExchangeEntity> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<DirectionExchangeEntity> Get(Func<DirectionExchangeEntity, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    public async Task<IEnumerable<DirectionExchangeEntity>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IEnumerable<DirectionExchangeEntity> GetWitchInclude(params Expression<Func<DirectionExchangeEntity, object>>[] includeProperties)
    {
        return Include(includeProperties);
    }

    public IEnumerable<DirectionExchangeEntity> GetWitchInclude(Func<DirectionExchangeEntity, bool> predicate, params Expression<Func<DirectionExchangeEntity, object>>[] includeProperties)
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

    public int Update(DirectionExchangeEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(DirectionExchangeEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    private IQueryable<DirectionExchangeEntity> Include(params Expression<Func<DirectionExchangeEntity, object>>[] includeProperties)
    {
        IQueryable<DirectionExchangeEntity> query = _dbSet.AsNoTracking();

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

    }
}
