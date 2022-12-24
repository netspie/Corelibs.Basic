using Common.Basic.DDD;

namespace Corelibs.Basic.Repository
{
    public class JsonEntity : IEntity
    {
        public JsonEntity() { }

        public string ID { get; init; } = new("");
        public string Content { get; init; } = new("");
        public uint Version { get; set; }
    }
}
