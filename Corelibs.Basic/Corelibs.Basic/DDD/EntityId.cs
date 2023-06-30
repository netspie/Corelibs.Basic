namespace Corelibs.Basic.DDD;

public abstract record EntityId(string Value)
{
    public static T New<T>() => (T) Activator.CreateInstance(typeof(T), Guid.NewGuid().ToString())!;
    public bool IsValid() => Guid.TryParse(Value, out var id);
    public override string ToString() => Value;
    public static implicit operator string(EntityId id) => id.ToString();
}
