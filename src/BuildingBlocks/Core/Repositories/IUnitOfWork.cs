namespace ModularMonolith.BuildingBlocks.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
