using Corelibs.Basic.Blocks;
using Corelibs.Basic.DDD;
using Corelibs.Basic.Json;

namespace Corelibs.Basic.Repository
{
    public class HttpClientRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly HttpClient _client;
        private readonly string _dataPath;
        private readonly IJsonConverter _jsonConverter;

        public HttpClientRepository(HttpClient client, string dataPath, IJsonConverter jsonConverter)
        {
            _client = client;
            _dataPath = dataPath;
            _jsonConverter = jsonConverter;
        }

        public Task<Result> Clear()
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ExistsOfName(string name, Func<TEntity, string> getName)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TEntity[]>> GetAll()
        {
            var filesStr = await _client.GetStringAsync(_dataPath + "/files");
            var files = filesStr.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None).ToArray();

            var fileTexts = await Task.WhenAll(files.Select(fn => _client.GetStringAsync(_dataPath + "/" + fn)).ToArray());
            var entities = fileTexts.Select(ft => _jsonConverter.Deserialize<TEntity>(ft)).ToArray();

            return Result<TEntity[]>.Success(entities);
        }

        public async Task<Result<TEntity[]>> GetAll(Action<int> setProgress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TEntity>> GetBy(string id)
        {
            var filePath = await _client.GetStringAsync(_dataPath + $"/{id}");
            var entity = _jsonConverter.Deserialize<TEntity>(filePath);

            if (entity == null)
                return Result<TEntity>.Failure();

            return Result<TEntity>.Success(entity);
        }

        public Task<Result<TEntity[]>> GetBy(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TEntity>> GetOfName(string name, Func<TEntity, string> getName)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Save(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
