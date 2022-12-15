using System;

namespace Common.Basic.Mapping
{
    public interface ITypeMapper
    {
        void Register<T>(Func<T> obj) where T : class;
        
        T Get<T>() where T : class;

        object Get(Type type);
    }
}
