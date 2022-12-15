using Common.Basic.Reflection;
using System.Linq;
using System.Reflection;

namespace Common.Basic.DI.Zenject
{
    public static class ZenjectExtensions
    {
        public static void BindAllFactories<TFactoryBase>(this object container, Assembly assembly)
            where TFactoryBase : class
        {
            var bindFactoryMethod = container
                .GetType()
                .GetMethods()
                .FirstOrDefault(m => m.Name == "BindFactory" && m.GetGenericArguments().Length == 2);

            var allFactories = AssemblyExtensionsEx.GetCurrentAssemblyTypesDerivedFrom<TFactoryBase>(assembly);
            foreach (var factory in allFactories)
            {
                if (factory.BaseType.GenericTypeArguments.Length != 1)
                    continue;

                var genericBindFactoryMethod = bindFactoryMethod.MakeGenericMethod(factory.BaseType.GenericTypeArguments[0], factory);
                genericBindFactoryMethod.Invoke(container, null);
            }
        }
    }
}