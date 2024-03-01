using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExchangeService.Database.Repositories;

public class DirectionsOperationRepository : IRepository<DirectionOperationEntity>
{
    private readonly DbSet<DirectionOperationEntity> _dbSet;
    private readonly ExchangeServiceContext _context;

    public DirectionsOperationRepository(ExchangeServiceContext context)
    {
        _context = context;
        _dbSet = _context.Set<DirectionOperationEntity>();
    }

    public int Create(DirectionOperationEntity item)
    {
        _dbSet.Add(item);

        return _context.SaveChanges();
    }

    public async Task<int> CreateAsync(DirectionOperationEntity item)
    {
        _dbSet.Add(item);

        return await _context.SaveChangesAsync();
    }

    public DirectionOperationEntity FindById(Guid id)
    {
        return _dbSet.FirstOrDefault(a => a.Id == id)!;
    }

    public async Task<DirectionOperationEntity> FindByIdAsync(Guid id)
    {
        return (await _dbSet.FirstOrDefaultAsync(a => a.Id == id))!;
    }

    public IEnumerable<DirectionOperationEntity> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<DirectionOperationEntity> Get(Func<DirectionOperationEntity, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    public async Task<IEnumerable<DirectionOperationEntity>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IEnumerable<DirectionOperationEntity> GetWitchInclude(params Expression<Func<DirectionOperationEntity, object>>[] includeProperties)
    {
        return Include(includeProperties);
    }

    public IEnumerable<DirectionOperationEntity> GetWitchInclude(Func<DirectionOperationEntity, bool> predicate, params Expression<Func<DirectionOperationEntity, object>>[] includeProperties)
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

    public int Update(DirectionOperationEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(DirectionOperationEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    private IQueryable<DirectionOperationEntity> Include(params Expression<Func<DirectionOperationEntity, object>>[] includeProperties)
    {
        IQueryable<DirectionOperationEntity> query = _dbSet.AsNoTracking();

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
