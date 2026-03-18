using MediatR;

namespace ModularMonolith.BuildingBlocks.Core.CQRS;

public interface IQuery<out T> : IRequest<T>
    where T : notnull
{
}
