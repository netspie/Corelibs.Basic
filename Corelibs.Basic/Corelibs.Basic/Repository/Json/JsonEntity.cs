using Common.Basic.DDD;

namespace Corelibs.Basic.Repository
{
    public class JsonEntity : IEntity
    {
        public JsonEntity() { }

        public string ID { get; set; } = new("");
        public string Content { get; set; } = new("");
        public uint Version { get; set; }
    }
}
