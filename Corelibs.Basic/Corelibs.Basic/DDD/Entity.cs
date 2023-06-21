using Corelibs.Basic.Collections;

namespace Corelibs.Basic.DDD
{
    public abstract class Entity : IEntity
    {
        public Entity() { }

        public Entity(string id)
        {
            ID = id;
        }

        public string ID { get; init; } = new("");

        public uint Version { get;  set; }
        uint IEntity.Version { get => Version; set { Version = value; } }

        public static implicit operator bool(Entity entity) => entity != null;

        public override string ToString()
        {
            var result = ID;

            var type = GetType();
            var nameProperty = type.GetProperty("Name");
            if (nameProperty != null)
            {
                var name = nameProperty.GetValue(this) as string;
                if (!name.IsNullOrEmpty())
                    result = $"{name} - {ID}";
            }

            return result;
        }

        public static string GetRandomName() =>
            $"Item - {new string(Guid.NewGuid().ToString().Take(8).ToArray())}";
    }
}
