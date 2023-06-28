namespace Corelibs.Basic.DDD
{
    public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId>
    {
    }

    public interface IAggregateRoot
    {
        public const string DefaultCollectionName = "";
    }
}
