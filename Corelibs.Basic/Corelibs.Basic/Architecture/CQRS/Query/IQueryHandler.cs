using Common.Basic.Blocks;
using System.Threading.Tasks;

namespace Common.Basic.CQRS.Query
{
    public interface IQueryHandler<TQuery, TQueryOutput>
        where TQuery : IQuery
        where TQueryOutput : IQueryOutput
    {
        Task<Result<TQueryOutput>> Handle(TQuery query);
    }
}
