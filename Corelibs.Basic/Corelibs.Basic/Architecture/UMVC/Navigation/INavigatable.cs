using System;

namespace Common.Basic.UMVC
{
    public interface INavigatable
    {
        bool OnPush();
        void OnPop();
        void OnStash(INavigatable navigatable);
        bool OnUnstash(INavigatable poped);

        Type Type { get; }
    }

    public interface INavigatable<TState> : INavigatable
    {
        TState State { get; }
    }
}
