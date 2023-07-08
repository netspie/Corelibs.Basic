namespace Corelibs.Basic.DDD
{
    public interface IEntity<TId>
        where TId : EntityId
    {
        TId Id { get; }
        uint Version { get; set; }
    }
}
