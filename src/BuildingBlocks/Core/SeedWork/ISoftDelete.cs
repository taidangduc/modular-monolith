namespace ModularMonolith.BuildingBlocks.Core.SeedWork;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
