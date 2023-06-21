using Corelibs.Basic.Blocks;

namespace Corelibs.Basic.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get resource by identification string.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success result with given type of resource. If none exists then result is also a success, but with no resource inside. If unexpected failure, then false.</returns>
        Task<Result<T>> GetBy(string id);
        Task<Result<T[]>> GetBy(IList<string> ids);
        Task<Result<T>> GetOfName(string name, Func<T, string> getName);
        Task<Result<T[]>> GetAll();
        Task<Result<T[]>> GetAll(Action<int> setProgress, CancellationToken ct);
        Task<Result> Save(T item);
        Task<Result> Clear();
        Task<Result> Delete(string id);

        Task<Result<bool>> ExistsOfName(string name, Func<T, string> getName);
    }
}
