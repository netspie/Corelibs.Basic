namespace Common.Basic.UMVC
{
    public interface INavigator
    {
        void Push(INavigatable navigated, bool onStashFirst = false, bool unstashPrevious = true);
        void PushLast();
        void Pop(bool unstashPrevious = true);

        INavigatable GetLast();
    }

    public static class INavigatorExtensions
    {
        public static void PopAndPushLast(this INavigator navigator)
        {
            navigator.Pop();
            navigator.PushLast();
        }
    }
}
