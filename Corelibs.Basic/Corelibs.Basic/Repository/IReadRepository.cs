using Common.Basic.Blocks;

namespace Common.Basic.Repository
{
    public interface IReadRepository
    {
        Task GetBy(string id, Result result);
    }
}
