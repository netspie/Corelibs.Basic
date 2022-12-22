using Common.Basic.Blocks;
using Common.Basic.DDD;
using Common.Basic.Repository;
using Microsoft.EntityFrameworkCore;

namespace Corelibs.Basic.Repository
{
    public class DbContextRepository<T, TDbContext> : IRepository<T>
        where T : class, IEntity
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly string _tableName;

        public JsonDbContextRepository(TDbContext dbContext, string tableName)
        {
            _dbContext = dbContext;
            _tableName = tableName;
        }

        public Task<Result> Clear()
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ExistsOfName(string name, Func<T, string> getName)
        {
            throw new NotImplementedException();
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
            var set = GetSet();
            T entity = await set.FirstOrDefaultAsync(e => e.ID == id);
            if (entity == null)
                return Result<T>.Failure();

            return Result<T>.Success(entity);
        }

        public Task<Result<T[]>> GetBy(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Result<T>> GetOfName(string name, Func<T, string> getName)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Save(T item)
        {
            var set = GetSet();
            await set.AddAsync(item);

            var entriesAddedCount = await _dbContext.SaveChangesAsync();
            if (entriesAddedCount == 0)
                return Result.Failure();

            return Result.Success();
        }

        private DbSet<T> GetSet()
        {
            return _dbContext.Set<T>(_tableName);
        }
    }
}
