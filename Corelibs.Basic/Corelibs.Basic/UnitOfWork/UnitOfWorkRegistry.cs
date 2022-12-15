namespace Common.Basic.Common.Basic.UnitOfWork
{
    public abstract class UnitOfWorkRegistry : IUnitOfWork
    {
        protected readonly List<object> _registeredToAdd = new();
        protected readonly List<object> _registeredToDelete = new();
        protected readonly List<object> _registeredToModify = new();

        public void RegisterAdded(object @object) => _registeredToAdd.Add(@object);
        public void RegisterDeleted(object @object) => _registeredToDelete.Add(@object);
        public void RegisterModified(object @object) => _registeredToModify.Add(@object);
        
        public abstract Task Commit();
    }
}
