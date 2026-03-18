using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.BuildingBlocks.Core.Repositories;

public interface IRepository<T> 
    where T : IAggregate
{
    IUnitOfWork UnitOfWork { get; }
}
