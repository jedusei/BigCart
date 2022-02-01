using BigCart.UITests.Views;
using System;

namespace BigCart.UITests.Extensions
{
    public static class ViewExtensions
    {
        public static T Invoke<T>(this T view, Action action) where T : View
        {
            action();
            return view;
        }

        public static T Invoke<T>(this T view, Action<T> action) where T : View
        {
            action(view);
            return view;
        }
    }
}
