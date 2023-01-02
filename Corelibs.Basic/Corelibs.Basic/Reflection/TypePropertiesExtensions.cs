using Common.Basic.Collections;
using System.Reflection;

namespace Corelibs.Basic.Reflection
{
    public static class TypePropertiesExtensions
    {
        public static PropertyInfo[] GetProperties(this Type type, IList<string> names, bool caseSensitive = true)
        {
            if (!caseSensitive)
                names = names.ToLower();

            return type.GetProperties().Where(p => names.Contains(caseSensitive ? p.Name : p.Name.ToLower())).ToArray();
        }

        public static PropertyInfo GetProperty(this Type type, string name, bool caseSensitive)
        {
            if (caseSensitive)
                return type.GetProperty(name);

            return type.GetProperties(new[] { name }, caseSensitive).FirstOrDefault();
        }
    }
}
