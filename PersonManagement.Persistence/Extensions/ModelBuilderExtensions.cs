using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void EnableSoftDeleted(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "entity");
                var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var notDeleted = Expression.Not(property);
                var lambda = Expression.Lambda(notDeleted, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}