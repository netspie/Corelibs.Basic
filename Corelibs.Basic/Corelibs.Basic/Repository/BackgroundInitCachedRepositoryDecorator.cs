using Corelibs.Basic.Blocks;
using Corelibs.Basic.DDD;

namespace Corelibs.Basic.Repository
{
    public class BackgroundInitCachedRepositoryDecorator<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private static readonly string NOT_INITIALIZED_MSG = "Can't perform the operation. Full storage has not been yet initialized.";

        private readonly IRepository<TEntity> _repository;
        private readonly IRepository<TEntity> _cachedRepository;
        private readonly Func<Action<int>, CancellationToken, Task> _initRepo;

        private bool _repoInit;

        public BackgroundInitCachedRepositoryDecorator(
            IRepository<TEntity> repository, 
            IRepository<TEntity> cachedRepository,
            Func<Action<int>, CancellationToken, Task> initRepo)
        {
            _repository = repository;
            _cachedRepository = cachedRepository;
            _initRepo = initRepo;
        }

        Task<Result> IRepository<TEntity>.Clear()
        {
            throw new NotImplementedException();
        }

        Task<Result> IRepository<TEntity>.Delete(string id)
        {
            if (!_repoInit)
                return Result.FailureTask(NOT_INITIALIZED_MSG);

            return _cachedRepository.Delete(id);
        }

        Task<Result<bool>> IRepository<TEntity>.ExistsOfName(string name, Func<TEntity, string> getName)
        {
            if (!_repoInit)
                return Result<bool>.FailureTask(NOT_INITIALIZED_MSG);

            return _cachedRepository.ExistsOfName(name, getName);
        }

        Task<Result<TEntity[]>> IRepository<TEntity>.GetAll()
        {
            if (!_repoInit)
                return Result<TEntity[]>.FailureTask(NOT_INITIALIZED_MSG);

            return _cachedRepository.GetAll();
        }

        Task<Result<TEntity[]>> IRepository<TEntity>.GetAll(Action<int> setProgress, CancellationToken ct)
        {
            if (!_repoInit)
                return Result<TEntity[]>.FailureTask(NOT_INITIALIZED_MSG);

            return _cachedRepository.GetAll(setProgress, ct);
        }

        Task<Result<TEntity>> IRepository<TEntity>.GetBy(string id)
        {
            if (!_repoInit)
                return _repository.GetBy(id);

            return _cachedRepository.GetBy(id);
        }

        async Task<Result> IRepository<TEntity>.Save(TEntity item)
        {
            if (!_repoInit)
                return Result.Failure(NOT_INITIALIZED_MSG);

            return _cachedRepository.Save(item);
        }

        public Task<Result<TEntity>> GetOfName(string name, Func<TEntity, string> getName)
        {
            if (!_repoInit)
                return Result<TEntity>.FailureTask(NOT_INITIALIZED_MSG);

            return _cachedRepository.GetOfName(name, getName);
        }

        public void Init(Action<int> setProgress, CancellationToken ct)
        {
            Task.Run(async () =>
            {
                await _initRepo(setProgress, ct);
                _repoInit = true;
            });
        }

        public Task<Result<TEntity[]>> GetBy(IList<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
