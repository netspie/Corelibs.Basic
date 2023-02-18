using Common.Basic.Blocks;
using Common.Basic.DDD;
using Common.Basic.Functional;
using Common.Basic.Json;
using Common.Basic.Repository;

namespace Corelibs.Basic.Repository
{
    public class JsonEntityRepositoryDecorator<TEntity, TDataEntity> : IRepository<TEntity>, IReadRepository
        where TEntity : class, IEntity
        where TDataEntity : JsonEntity, new()
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IRepository<TDataEntity> _jsonTableRepository;

        public JsonEntityRepositoryDecorator(
            IRepository<TDataEntity> jsonTableRepository)
        {
            _jsonConverter = new NewtonsoftJsonConverter();
            _jsonTableRepository = jsonTableRepository;
        }

        public Task<Result> Clear()
        {
            throw new NotImplementedException(); // to delete?
        }

        public Task<Result> Delete(string id)
        {
            return _jsonTableRepository.Delete(id);
        }

        public Task<Result<bool>> ExistsOfName(string name, Func<TEntity, string> getName)
        {
            throw new NotImplementedException(); // to delete?
        }

        public async Task<Result<TEntity[]>> GetAll()
        {
            var result = Result<TEntity[]>.Success();

            var jsonEntitiesResult = await _jsonTableRepository.GetAll();
            if (!jsonEntitiesResult.ValidateSuccessAndValues())
                return result.Fail().With(jsonEntitiesResult);

            var jsonEntities = jsonEntitiesResult.Get();
            var entities = jsonEntities.Select(e => _jsonConverter.Deserialize<TEntity>(e.Content)).ToArray();
            return Result<TEntity[]>.Success(entities);
        }

        public Task<Result<TEntity[]>> GetAll(Action<int> setProgress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TEntity>> GetBy(string id)
        {
            var jsonEntityResult = await _jsonTableRepository.GetBy(id);
            if (!jsonEntityResult.IsSuccess)
                return Result<TEntity>.Failure().With(jsonEntityResult);

            var jsonEntity = jsonEntityResult.Get();
            if (jsonEntity.IsNull())
                return Result<TEntity>.Success();

            var entity = _jsonConverter.Deserialize<TEntity>(jsonEntity.Content);

            return Result<TEntity>.Success(entity);
        }

        public async Task GetBy(string id, Result result)
        {
            var resultLocal = await GetBy(id);
            result.Add(resultLocal);
        }

        public async Task<Result<TEntity[]>> GetBy(IList<string> ids)
        {
            var result = Result<TEntity[]>.Success();

            var jsonEntitiesResult = await _jsonTableRepository.GetBy(ids);
            if (!jsonEntitiesResult.ValidateSuccessAndValues())
                return result.Fail().With(jsonEntitiesResult);

            var jsonEntities = jsonEntitiesResult.Get();
            var entities = jsonEntities.Select(e => _jsonConverter.Deserialize<TEntity>(e.Content)).ToArray();
            return Result<TEntity[]>.Success(entities);
        }

        public Task<Result<TEntity>> GetOfName(string name, Func<TEntity, string> getName)
        {
            throw new NotImplementedException(); // to delete?
        }

        public async Task<Result> Save(TEntity item)
        {
            var result = Result.Success();

            var json = _jsonConverter.Serialize(item);

            var entity = await _jsonTableRepository.Get(item.ID, result);
            if (entity != null)
            {
                entity.Content = json;
                entity.Version = item.Version++;
            }
            else
            {
                entity = new TDataEntity()
                {
                    ID = item.ID,
                    Content = json
                };
            }

            return _jsonTableRepository.Save(entity);
        }
    }
}
