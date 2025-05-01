using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain.Entities;
using PersonManagment.Application.Abstractions.Repositories;

namespace PersonManagement.Persistence.Repositories;

public class GenericRepository<TEntity, TEntityId>(PersonDbContext dbContext)
    : IGenericRepository<TEntity, TEntityId> where TEntity : BaseEntity<TEntityId>
{
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }
}