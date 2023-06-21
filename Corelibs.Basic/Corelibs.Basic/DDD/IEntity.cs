namespace Corelibs.Basic.DDD
{
    public interface IEntity<TId>
    {
        TId Id { get; }
        uint Version { get; set; }
    }
}
