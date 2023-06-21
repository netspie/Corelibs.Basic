using Corelibs.Basic.Blocks;

namespace Corelibs.Basic.Repository
{
    public interface IReadRepository
    {
        Task GetBy(string id, Result result);
    }
}
