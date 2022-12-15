using Common.Basic.Collections;
using System;
using System.Linq;

namespace Common.Basic.Functional
{
    public static class DFuncExt
    {
        public static Action<T> And<T>(this Action<T> action, params Action<T>[] otherActions)
        {
            return param =>
            {
                action(param);
                otherActions.ForEach(a => a(param));
            };
        }
        
        public static T As<T>(this object @object)
            where T : class
            => @object as T;

        public static T If<T>(this T @object, params Func<T, bool>[] conditions)
        {
            if (conditions.All(condition => condition(@object)))
                return @object;

            return default;
        }

        public static TFirstConditionReturn If<T, TFirstConditionReturn>(
            this T @object, Func<T, TFirstConditionReturn> condition1, params Func<T, bool>[] conditions)
        {
            var result = condition1(@object);
            if (result == null)
                return default;

            if (conditions.Any(condition => !condition(@object)))
                return default;

            return result;
        }

        public static T DoIf<T, TFirstConditionReturn>(
            this T @object, Action<TFirstConditionReturn> action, Func<T, TFirstConditionReturn> condition1, params Func<T, bool>[] conditions)
        {
            var result = condition1(@object);
            if (result == null)
                return default;

            if (conditions.Any(condition => !condition(@object)))
                return default;

            action(result);

            return @object;
        }

        public static T DoIf<T, TFirstConditionReturn>(
            this T @object, Func<T, TFirstConditionReturn> condition1, Action<TFirstConditionReturn> action)
        {
            var result = condition1(@object);
            if (result == null)
                return default;

            action(result);

            return @object;
        }

        public static T DoIf<T, TFirstConditionReturn>(
            this T @object, Func<T, TFirstConditionReturn> condition1, Func<T, bool> condition2, Action<TFirstConditionReturn> action)
        {
            var result = condition1(@object);
            if (result == null)
                return default;

            if (!condition2(@object))
                return default;

            action(result);

            return @object;
        }

        public static T DoIf<T, TFirstConditionReturn>(
            this T @object, Func<T, TFirstConditionReturn> condition1, Func<T, bool> condition2, Func<T, bool> condition3, Action<TFirstConditionReturn> action)
        {
            var result = condition1(@object);
            if (result == null)
                return default;

            if (!condition2(@object))
                return default;

            if (!condition3(@object))
                return default;

            action(result);

            return @object;
        }

        public static T Then<T>(this T value, Action action)
        {
            if (value != null)
                action();

            return value;
        }

        public static T Then<T>(this T value, Action<T> action)
        {
            if (value != null)
                action(value);

            return value;
        }

        public static T Then<T>(this Action action1, Func<T> action2)
        {
            if (action1 != null)
                action1();

            return action2();
        }

        public static T IfOkayThen<T>(this T @object, Action<T> action)
        {
            if (@object != null)
                action(@object);

            return @object;
        }

        public static T IfNullThen<T>(this T @object, Action action)
        {
            if (@object == null)
                action();

            return @object;
        }

        public static T IfOkThen<T>(this T @object, Action action)
        {
            if (@object != null)
                action();

            return @object;
        }

        public static T IfOkOrNot<T>(this T @object, Action actionOnOk, Action actionOnNull)
        {
            if (@object != null)
                actionOnOk();
            else
                actionOnNull();

            return @object;
        }

        public static bool Not(this bool value)
        {
            return !value;
        }

        public static Func<T, bool> Not<T>(this Func<T, bool> function)
        {
            return value => !function(value);
        }

        public static bool ReturnTrue<T>(this T action) => true;

        public static bool IsOfType<T>(this object @object)
        {
            var objectName = @object.GetType().FullName;
            var checkTypeName = typeof(T).FullName;

            return objectName == checkTypeName;
        }
    }
}
