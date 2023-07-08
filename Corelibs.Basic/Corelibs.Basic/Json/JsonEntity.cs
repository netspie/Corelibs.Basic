using Corelibs.Basic.DDD;

namespace Corelibs.Basic.Json
{
    public class JsonEntity<TEntityId> : IEntity<TEntityId>
        where TEntityId : EntityId
    {
        public JsonEntity() {}

        public TEntityId Id { get; set; }

        public string Content { get; set; }
        public uint Version { get; set; }
    }
}
