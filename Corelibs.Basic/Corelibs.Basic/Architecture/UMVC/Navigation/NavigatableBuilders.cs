using System;

namespace Common.Basic.UMVC
{
    public static class NavigatableBuilders
    {
        public static INavigatable BuildPushPopUnstash<TObject>(
            Action load,
            Action unload)
        {
            return new Navigatable<TObject>(
                 onPush: () => { load(); return true; },
                 onPop: unload,
                 onStash: Navigatable.OnStashDefault,
                 onUnstash: nav =>
                 {
                     unload();
                     load();
                     return true;
                 });
        }

        public static INavigatable BuildPushPop<TObject>(
            Action load,
            Action unload)
        {
            return new Navigatable<TObject>(
                 onPush: () => { load(); return true; },
                 onPop: unload,
                 onStash: Navigatable.OnStashDefault,
                 onUnstash: Navigatable.OnUnstashDefault);
        }

        public static INavigatable BuildPushPopUnstash<TObject, TState>(
            Action<TState> load,
            Action<TState> unload) where TState : class, new()

        {
            return new NavigatableWithState<TObject, TState>(
                 onPush: state => { load(state); return true; },
                 onPop: state => unload(state),
                 onStash: (state, nav) => Navigatable.OnStashDefault(nav),
                 onUnstash: (state, nav) =>
                 {
                     unload(state);
                     load(state);
                     return true;
                 });
        }

        public static INavigatable BuildPushPopStashUnstash(Action load, Action unload)
        {
            return new Navigatable(
                 onPush: () => { load(); return true; },
                 onPop: () => unload(),
                 onStash: (nav) => unload(),
                 onUnstash: (nav) =>
                 {
                     load();
                     return true;
                 });
        }

        public static INavigatable BuildPushPopStashUnstash<TObject, TState>(
            Action<TState> load,
            Action<TState> unload) where TState : class, new()
        {
            return new NavigatableWithState<TObject, TState>(
                 onPush: state => { load(state); return true; },
                 onPop: state => unload(state),
                 onStash: (state, nav) => unload(state),
                 onUnstash: (state, nav) =>
                 {
                     load(state);
                     return true;
                 });
        }

        public static INavigatable BuildPushPop<TObject, TState>(
            Action<TState> load,
            Action unload) where TState : class, new()
        {
            return new NavigatableWithState<TObject, TState>(
                 onPush: state => { load(state); return true; },
                 onPop: state => unload(),
                 onStash: (state, nav) => Navigatable.OnStashDefault(nav),
                 onUnstash: (state, nav) => Navigatable.OnUnstashDefault(nav));
        }
    }
}
