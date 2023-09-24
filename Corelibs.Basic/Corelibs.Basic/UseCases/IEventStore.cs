using Corelibs.Basic.Blocks;
using Corelibs.Basic.DDD;

namespace Corelibs.Basic.UseCases;

public interface IEventStore
{
    Task<Result> Save(BaseDomainEvent ev);
    Task<Result> Save(IEnumerable<BaseDomainEvent> events);
    Task<Result> Delete(BaseDomainEvent ev);
    Task<Result<BaseDomainEvent[]>> Peek(int count = 1);
}
