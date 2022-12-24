using Common.Basic.Blocks;
using Common.Basic.DDD;
using Common.Basic.Repository;
using Microsoft.EntityFrameworkCore;

namespace Corelibs.Basic.Repository
{
    public class DbContextRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;

        public DbContextRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
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

        public Task<Result<TEntity[]>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<TEntity[]>> GetAll(Action<int> setProgress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TEntity>> GetBy(string id)
        {
            var set = GetSet();
            TEntity entity = await set.FirstOrDefaultAsync(e => e.ID == id);
            if (entity == null)
                return Result<TEntity>.Success();

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

        public async Task<Result> Save(TEntity item)
        {
            var set = GetSet();
            await set.AddAsync(item);

            var entriesAddedCount = await _dbContext.SaveChangesAsync();
            if (entriesAddedCount == 0)
                return Result.Failure();

            return Result.Success();
        }

        private DbSet<TEntity> GetSet()
        {
            return _dbContext.Set<TEntity>();
        }
    }
}
