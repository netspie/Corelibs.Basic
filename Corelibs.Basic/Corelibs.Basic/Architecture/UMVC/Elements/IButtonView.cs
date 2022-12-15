using System;

namespace Common.Basic.UMVC.Elements
{
    public interface IButtonView : IView
    {
        Action OnUp { set; }

        bool Interactable { set; }

        public static class Extensions
        {
            public static Action<TButtonView> InitButton<TButtonView>(Action onUp, Enum type)
                where TButtonView : IButtonView
            {
                return btn =>
                {
                    btn.OnUp = btn.OnUp = onUp;
                    btn.ID = btn.ID = type.ToString();
                };
            }
        }
    }
}
