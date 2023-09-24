namespace Corelibs.Basic.DDD;

public class BaseDomainEvent : IDomainEvent
{
    public required string Id { get; init; }

    public required long Timestamp { get; init; }
}
