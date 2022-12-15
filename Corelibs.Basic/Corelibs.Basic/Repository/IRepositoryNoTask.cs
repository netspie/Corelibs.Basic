using Common.Basic.Blocks;
using System;

namespace Common.Basic.Repository
{
    public interface IRepositoryNoTask<T>
    {
        /// <summary>
        /// Get resource by identification string.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success result with given type of resource. If none exists then result is also a success, but with no resource inside. If unexpected failure, then false.</returns>
        Result<T> GetBy(string id);
        Result<T[]> GetAll();
        Result Save(T item);
        Result Clear();
        Result Delete(string id);

        Result<bool> ExistsOfName(string name, Func<T, string> getName);
    }
}
