using System;
using System.Linq;
using System.Reflection;

namespace Common.Basic.Reflection
{
    public static class AssemblyExtensionsEx
    {
        public static Type[] GetCurrentAssemblyTypesDerivedFrom<T>(this Assembly assembly)
            where T : class
        {
            var types = assembly.GetTypes();
            var classTypes = types.Where(t => t.IsClass).ToArray();
            var assignableFromTTypes = classTypes.Where(t => typeof(T).IsAssignableFrom(t)).ToArray();

            return assignableFromTTypes;
        }
    }
}
