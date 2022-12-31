namespace Common.Basic.Repository
{
    public interface IAccessor<out T>
    {
        T Get();
    }
}
