using System.Linq.Expressions;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Models;

namespace TreesApi.DataAccess.Repositories.Generic;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        params string[] includeProperties);

    Task<TEntity?> GetByIdAsync(long id, params string[] includeProperties);
    Task<PageableResult<TEntity>> GetPageableAsync(int skip, int take, Expression<Func<TEntity, bool>>? filter);
    Task InsertAsync(TEntity entity);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
}
