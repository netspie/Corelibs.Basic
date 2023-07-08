namespace Corelibs.Basic.DDD
{
    public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId>
        where TId : EntityId
    {
    }

    public interface IAggregateRoot
    {
        public const string DefaultCollectionName = "";
    }
}
