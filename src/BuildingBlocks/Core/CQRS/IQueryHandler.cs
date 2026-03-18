using MediatR;

namespace ModularMonolith.BuildingBlocks.Core.CQRS;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
    where TResult : notnull
{
}
