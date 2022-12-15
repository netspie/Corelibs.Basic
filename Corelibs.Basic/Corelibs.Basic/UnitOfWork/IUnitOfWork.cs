namespace Common.Basic.Common.Basic.UnitOfWork;

public interface IUnitOfWork
{
    void RegisterAdded(object @object);
    void RegisterDeleted(object @object);
    void RegisterModified(object @object);

    Task Commit();
}
