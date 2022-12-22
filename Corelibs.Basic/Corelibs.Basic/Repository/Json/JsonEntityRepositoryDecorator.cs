using Common.Basic.Blocks;
using Common.Basic.DDD;
using Common.Basic.Json;
using Common.Basic.Repository;

namespace Corelibs.Basic.Repository
{
    public class JsonEntityRepositoryDecorator<T, TDbContext> : IRepository<T>
       where T : class, IEntity
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IRepository<JsonEntity> _jsonTableRepository;

        public JsonEntityRepositoryDecorator(IJsonConverter jsonConverter, IRepository<JsonEntity> jsonTableRepository)
        {
            _jsonConverter = jsonConverter;
            _jsonTableRepository = jsonTableRepository;
        }

        public Task<Result> Clear()
        {
            throw new NotImplementedException(); // to delete?
        }

        public Task<Result> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ExistsOfName(string name, Func<T, string> getName)
        {
            throw new NotImplementedException(); // to delete?
        }

        public Task<Result<T[]>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<T[]>> GetAll(Action<int> setProgress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<T>> GetBy(string id)
        {
            var jsonEntityResult = await _jsonTableRepository.GetBy(id);
            if (!jsonEntityResult.IsSuccess)
                return Result<T>.Failure().With(jsonEntityResult);

            var jsonEntity = jsonEntityResult.Get();
            var entity = _jsonConverter.Deserialize<T>(jsonEntity.Content);

            return Result<T>.Success(entity);
        }

        public Task<Result<T[]>> GetBy(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> GetOfName(string name, Func<T, string> getName)
        {
            throw new NotImplementedException(); // to delete?
        }

        public Task<Result> Save(T item)
        {
            var json = _jsonConverter.Serialize(item);
            var jsonEntity = new JsonEntity(item.ID, json);

            return _jsonTableRepository.Save(jsonEntity);
        }
    }
}
