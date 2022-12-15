using System;
using System.Collections.Generic;

namespace Common.Basic.DI
{
    public class DependencyContainer : IDependencyContainer
    {
        private IDictionary<Type, object> _dependencies = new Dictionary<Type, object>();
        private IDictionary<string, KeyValuePair<Type, object>> _dependenciesWithId = new Dictionary<string, KeyValuePair<Type, object>>();

        T IDependencyContainer.Register<T>(T obj)
        {
            if (_dependencies.ContainsKey(typeof(T)))
                _dependencies.Remove(typeof(T));

            _dependencies.Add(typeof(T), obj);
            return obj;
        }

        T IDependencyContainer.Get<T>()
        {
            return Get(typeof(T)) as T;
        }

        public object Get(Type type)
        {
            if (!_dependencies.ContainsKey(type))
                return default;

            return _dependencies[type];
        }

        TBase IDependencyContainer.Register<TBase, TDerived>()
        {
            return (this as IDependencyContainer).Register<TBase>(new TDerived());
        }

        T IDependencyContainer.Register<T>(T obj, string id)
        {
            if (_dependenciesWithId.ContainsKey(id))
                _dependenciesWithId.Remove(id);

            _dependenciesWithId.Add(id, new KeyValuePair<Type, object>(typeof(T), obj));
            return obj;
        }

        T IDependencyContainer.Get<T>(string id)
        {
            if (_dependenciesWithId.ContainsKey(id))
                return _dependenciesWithId[id].Value as T;
            
            return default;
        }

        void IDependencyContainer.Remove<T>()
        {
            var type = typeof(T);

            if (_dependencies.ContainsKey(type))
            {
                _dependencies.Remove(type);
                return;
            }
        }

        void IDependencyContainer.Remove(string id)
        {
            if (_dependenciesWithId.ContainsKey(id))
            {
                _dependenciesWithId.Remove(id);
                return;
            }
        }

        void IDependencyContainer.Clear()
        {
            _dependencies.Clear();
            _dependenciesWithId.Clear();
        }
    }
}
