using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Models;

namespace TreesApi.DataAccess.Repositories.Generic;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        params string[] includeProperties)
    {
        var query = Include(_dbSet, includeProperties);
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }
    
    public async Task<PageableResult<TEntity>> GetPageableAsync(int skip, int take, Expression<Func<TEntity, bool>>? filter)
    {
        var result = new PageableResult<TEntity>
        {
            Skip = skip,
        };

        IQueryable<TEntity> query = _dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        result.Items = await query.OrderBy(e => e.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        result.Count = await query.CountAsync();

        return result;
    }

    public async Task<TEntity?> GetByIdAsync(long id, params string[] includeProperties)
    {
        return (await GetAsync(e => e.Id == id, includeProperties)).SingleOrDefault();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    private static IQueryable<TEntity> Include(IQueryable<TEntity> query, string[] includeProperties)
    {
        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
