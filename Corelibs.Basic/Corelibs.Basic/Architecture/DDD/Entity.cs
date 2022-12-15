namespace Common.Basic.DDD
{
    public abstract class Entity : IEntity
    {
        public string ID { get; init; }

        public uint Version { get; private set; }
        uint IEntity.Version { get => Version; set { Version = value; } }

        public static implicit operator bool(Entity entity) => entity != null;
    }
}
