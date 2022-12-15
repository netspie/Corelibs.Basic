using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.DDD;

namespace Common.Basic.Repository;

public class CachedInMemoryRepositoryDecorator<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;
    private List<TEntity> _cached;

    public CachedInMemoryRepositoryDecorator(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    Task<Result> IRepository<TEntity>.Clear()
    {
        throw new NotImplementedException();
    }

    async Task<Result> IRepository<TEntity>.Delete(string id)
    {
        if (_cached == null)
            return Result.Failure();

        var result = await _repository.Delete(id);
        if (!result.IsSuccess)
            return result;

        _cached.RemoveFirstIf(e => e.ID == id);
        return Result.Success();
    }

    Task<Result<bool>> IRepository<TEntity>.ExistsOfName(string name, Func<TEntity, string> getName)
    {
        if (_cached == null)
            return Result<bool>.FailureTask();

        bool exists = _cached.FirstOrDefault(e => getName(e) == name) != null;
        return Result<bool>.SuccessTask(exists);
    }

    Task<Result<TEntity[]>> IRepository<TEntity>.GetAll()
    {
        if (_cached == null)
            return Result<TEntity[]>.FailureTask();

        return Result<TEntity[]>.SuccessTask(_cached.ToArray());
    }

    Task<Result<TEntity[]>> IRepository<TEntity>.GetAll(Action<int> setProgress, CancellationToken ct)
    {
        if (_cached == null)
            return Result<TEntity[]>.FailureTask();

        return Result<TEntity[]>.SuccessTask(_cached.ToArray());
    }

    Task<Result<TEntity>> IRepository<TEntity>.GetBy(string id)
    {
        if (_cached == null)
            return Result<TEntity>.FailureTask();

        var entity = _cached.FirstOrDefault(e => e.ID == id);
        return Result<TEntity>.SuccessTask(entity);
    }

    async Task<Result> IRepository<TEntity>.Save(TEntity item)
    {
        if (_cached == null)
            return Result.Failure();

        var result = await _repository.Save(item);
        if (!result.IsSuccess)
            return result;

        var entity = _cached.FirstOrDefault(e => e.ID == item.ID);
        if (entity != null)
            _cached.Remove(entity);

        _cached.Add(item);
        return Result.Success();
    }

    public Task<Result<TEntity>> GetOfName(string name, Func<TEntity, string> getName)
    {
        if (_cached == null)
            return Result<TEntity>.FailureTask();

        var entity = _cached.FirstOrDefault(e => getName(e) == name);
        return Result<TEntity>.SuccessTask(entity);
    }

    public Task InitAsync() =>
        Init(p => { }, new CancellationToken());

    public async Task Init(Action<int> setProgress, CancellationToken ct)
    {
        if (_cached != null)
            return;

        var result = await _repository.GetAll(setProgress, ct);
        var resultValue = result.Get();

        _cached = resultValue.ToList();
    }

    public void Init() =>
        Init(p => { }, new CancellationToken()).ConfigureAwait(false).GetAwaiter().GetResult();

    public Task<Result<TEntity[]>> GetBy(IList<string> ids)
    {
        throw new NotImplementedException();
    }
}
