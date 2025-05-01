using System.Linq.Expressions;
using PersonManagement.Domain.Entities;

namespace PersonManagment.Application.Abstractions.Repositories;

public interface IGenericRepository<TEntity, TEntityId> where TEntity : BaseEntity<TEntityId>
{
    Task<TEntity> AddAsync(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetByIdAsync(TEntityId id, params Expression<Func<TEntity, object>>[] includes);
}