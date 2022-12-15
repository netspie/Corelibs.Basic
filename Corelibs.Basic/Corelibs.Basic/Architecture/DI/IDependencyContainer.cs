using System;

namespace Common.Basic.DI
{
    public interface IDependencyContainer
    {
        T Register<T>(T obj) where T : class;

        object Get(Type type);
        T Get<T>() where T : class;

        T Register<T>(T obj, string id) where T : class;
        TBase Register<TBase, TDerived>()
            where TDerived : class, TBase, new()
            where TBase : class;

        T Get<T>(string id) where T : class;

        void Remove<T>();
        void Remove(string id);
        void Clear();
    }
}
