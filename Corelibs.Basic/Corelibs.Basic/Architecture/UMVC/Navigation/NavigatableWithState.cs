using System;

namespace Common.Basic.UMVC
{
    public class NavigatableWithState<TState> : INavigatable<TState>
        where TState : class, new()
    {
        public static Func<TState, bool> OnPushDefault = state => true;
        public static Action<TState> OnPopDefault = state => {};
        public static Action<TState, INavigatable> OnStashDefault = (state, navigatable) => {};
        public static Func<TState, INavigatable, bool> OnUnstashDefault = (state, poped) => true;

        private readonly Func<TState, bool> _onPush;
        private readonly Action<TState> _onPop;
        private readonly Action<TState, INavigatable> _onStash;
        private readonly Func<TState, INavigatable, bool> _onUnstash;

        private readonly TState _state = new TState();

        public NavigatableWithState(
            Type type,
            Func<TState, bool> onPush,
            Action<TState> onPop,
            Action<TState, INavigatable> onStash,
            Func<TState, INavigatable, bool> onUnstash,
            TState state = null)
        {
            Type = type;

            _onPush = onPush;
            _onPop = onPop;
            _onStash = onStash;
            _onUnstash = onUnstash;

            _state = state ?? _state;
        }

        public bool OnPush() => _onPush(_state);
        public void OnPop() => _onPop(_state);
        public void OnStash(INavigatable navigatable) => _onStash(_state, navigatable);
        public bool OnUnstash(INavigatable poped) => _onUnstash(_state, poped);
        public Type Type { get; }
        public TState State => _state;
    }

    public class NavigatableWithState<TNavigatable, TState> : NavigatableWithState<TState>
        where TState : class, new()
    {
        public NavigatableWithState(
            Func<TState, bool> onPush,
            Action<TState> onPop,
            Action<TState, INavigatable> onStash,
            Func<TState, INavigatable, bool> onUnstash,
            TState state = null)
            : base(typeof(TNavigatable), onPush, onPop, onStash, onUnstash, state) {}
    }
}
