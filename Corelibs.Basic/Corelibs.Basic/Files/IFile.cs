using Common.Basic.Blocks;
using System.Threading.Tasks;

namespace Common.Basic.Files
{
    public interface IFile<T>
        where T : class
    {
        Task<Result<T>> Get();
        Task<Result> Save(T @object);
    }
}
