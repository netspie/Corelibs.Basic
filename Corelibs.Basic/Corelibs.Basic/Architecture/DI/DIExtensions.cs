using System.Reflection;

namespace Common.Basic.DI
{
    public static class DIExtensions
    {
        public static void AssignPrivateReadonlyFieldsFromIOC(this IDependencyContainer dependencyContainer, object obj)
        {
            FieldInfo[] fieldInfos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var fieldInfo in fieldInfos)
            {
                if (!fieldInfo.IsInitOnly)
                    continue;

                object fieldValue = dependencyContainer.Get(fieldInfo.FieldType);
                if (fieldValue == null)
                    continue;

                fieldInfo.SetValue(obj, fieldValue);
            }
        }
    }
}
