using BuildingBlocks.Web;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ModularMonolith.BuildingBlocks.Core.SeedWork;
using System.Collections.Immutable;
using System.Data;

namespace BuildingBlocks.EFCore;

public class DbContextBase : DbContext
{
    private IDbContextTransaction _currentTransaction;
    private readonly ILogger<DbContextBase> _logger;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IPublisher _publisher;

    public DbContextBase(
        DbContextOptions options,
        ILogger<DbContextBase>? logger = null,
        ICurrentUserProvider? currentUserProvider = null,
        IPublisher? publisher = null)
        : base(options)
    {
        _logger = logger;
        _currentUserProvider = currentUserProvider;
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var domainEntities = ChangeTracker
                .Entries<IHasDomainEvent>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Count != 0)
                .ToImmutableList();

            await DispatchAndClearEvents(domainEntities);

            return await base.SaveChangesAsync(cancellationToken);
        }
        //ref: https://learn.microsoft.com/en-us/ef/core/saving/concurrency?tabs=data-annotations#resolving-concurrency-conflicts
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var databaseValues = await entry.GetDatabaseValuesAsync();

                if (databaseValues == null)
                {
                    _logger.LogWarning("The record no longer exists in the database, The record has been deleted by another user.");
                    throw;
                }

                // Refresh original values to bypass next concurrency check
                entry.CurrentValues.SetValues(databaseValues);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            await _currentTransaction?.CommitAsync(cancellationToken)!;
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public IExecutionStrategy CreateExcutionStragegy() => Database.CreateExecutionStrategy();

    //ref: https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency#execution-strategies-and-transactions
    public Task ExecutionTransactionAsync(CancellationToken cancellationToken = default)
    {
        var stategy = CreateExcutionStragegy();

        return stategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                    await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

        });
    }

    public async Task DispatchAndClearEvents(ImmutableList<IHasDomainEvent> domainEntities)
    {
        foreach (var entity in domainEntities)
        {
            if (entity is not HasDomainEvent hasDomainEvents)
            {
                continue;
            }

            DomainEvent[] events = hasDomainEvents.DomainEvents.ToArray();
            hasDomainEvents.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _currentTransaction?.RollbackAsync(cancellationToken)!;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }
}
