namespace Corelibs.Basic.Repository;

public interface IAccessor<out T>
{
    T Get();
}
