using System;

namespace Common.Basic.UMVC
{
    public class Navigatable : INavigatable
    {
        public static Func<bool> OnPushDefault = () => true;
        public static Action OnPopDefault = () => { };
        public static Action<INavigatable> OnStashDefault = (navigatable) => { };
        public static Func<INavigatable, bool> OnUnstashDefault = (poped) => true;

        private readonly Func<bool> _onPush;
        private readonly Action _onPop;
        private readonly Action<INavigatable> _onStash;
        private readonly Func<INavigatable, bool> _onUnstash;

        public Type Type { get; }

        public Navigatable(
            Type type,
            Func<bool> onPush, Action onPop, Action<INavigatable> onStash, Func<INavigatable, bool> onUnstash)
        {
            Type = type;

            _onPush = onPush;
            _onPop = onPop;
            _onStash = onStash;
            _onUnstash = onUnstash;
        }

        public Navigatable(
            Func<bool> onPush, Action onPop, Action<INavigatable> onStash, Func<INavigatable, bool> onUnstash)
            : this(typeof(object), onPush, onPop, onStash, onUnstash) {}

        public bool OnPush() => _onPush();
        public void OnPop() => _onPop();
        public void OnStash(INavigatable navigatable) => _onStash(navigatable);
        public bool OnUnstash(INavigatable poped) => _onUnstash(poped);
    }

    public class Navigatable<T> : Navigatable
    {
        public Navigatable(Func<bool> onPush, Action onPop, Action<INavigatable> onStash, Func<INavigatable, bool> onUnstash) 
            : base(typeof(T), onPush, onPop, onStash, onUnstash) {}
    }

}
