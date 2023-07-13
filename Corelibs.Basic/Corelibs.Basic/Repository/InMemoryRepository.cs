using Corelibs.Basic.Blocks;
using Corelibs.Basic.DDD;
using System.Collections.Concurrent;

namespace Corelibs.Basic.Repository;

public class InMemoryRepository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : IEntity<TEntityId>
    where TEntityId : EntityId
{
    private readonly ConcurrentDictionary<string, TEntity> _entities = new();

    public Task<Result> Clear()
    {
        _entities.Clear();
        return Result.SuccessTask();
    }

    public Task<Result> Delete(TEntityId id)
    {
        _entities.TryRemove(id, out var value);
        return Result.SuccessTask();
    }

    public Task<Result<bool>> ExistsOfName(string name, Func<TEntity, string> getName)
    {
        throw new NotImplementedException();
    }

    public Task<Result<TEntity[]>> GetAll()
    {
        return Task.FromResult(new Result<TEntity[]>(_entities.Values.ToArray()));
    }

    public Task<Result<TEntity[]>> GetAll(Action<int> setProgress, CancellationToken ct)
    {
        return Task.FromResult(new Result<TEntity[]>(_entities.Values.ToArray()));
    }

    public Task<Result<TEntity>> GetBy(TEntityId id)
    {
        if (!_entities.TryGetValue(id.Value, out var entity))
            return Result<TEntity>.FailureTask();

        return Task.FromResult(new Result<TEntity>(entity));
    }

    public Task<Result<TEntity[]>> GetBy(IList<TEntityId> ids)
    {
        return Task.FromResult(new Result<TEntity[]>(ids.Select(id => _entities[id]).ToArray()));
    }

    public Task<Result<TEntity>> GetOfName(string name, Func<TEntity, string> getName)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Save(TEntity item)
    {
        if (_entities.ContainsKey(item.Id))
            _entities[item.Id] = item;
        else
            _entities.TryAdd(item.Id, item);

        return Result.SuccessTask();
    }
}
