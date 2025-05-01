using Microsoft.EntityFrameworkCore.Storage;
using PersonManagment.Application.Abstractions;

namespace PersonManagement.Persistence;

public class UnitOfWork(PersonDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? transaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is not null)
        {
            await transaction.CommitAsync(cancellationToken);
            transaction.Dispose();
            transaction = null;
        }
        else
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is not null)
        {
            await transaction.RollbackAsync(cancellationToken);
            transaction.Dispose();
            transaction = null;
        }
        else
        {
            throw new InvalidOperationException("No active transaction to rollback.");
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        if (transaction is not null)
        {
            transaction.Dispose();
        }
    }
}