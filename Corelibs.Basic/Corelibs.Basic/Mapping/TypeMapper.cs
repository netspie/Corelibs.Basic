using System;
using System.Collections.Generic;

namespace Common.Basic.Mapping
{
    public class TypeMapper : ITypeMapper
    {
        private IDictionary<Type, Func<object>> _dependencies = new Dictionary<Type, Func<object>>();

        void ITypeMapper.Register<T>(Func<T> obj)
        {
            if (_dependencies.ContainsKey(typeof(T)))
                return;

            _dependencies.Add(typeof(T), obj);
        }

        T ITypeMapper.Get<T>()
        {
            if (!_dependencies.ContainsKey(typeof(T)))
                return default;

            var objCreator = _dependencies[typeof(T)];
            return objCreator.Invoke() as T;
        }

        object ITypeMapper.Get(Type type)
        {
            if (!_dependencies.ContainsKey(type))
                return default;

            var objCreator = _dependencies[type];
            return objCreator.Invoke();
        }
    }
}
