using System;
using System.Linq;
using Xamarin.Forms;

namespace BigCart.Effects
{
    [Flags]
    public enum Sides
    {
        None = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,
        All = ~None
    }

    public class SafeAreaEffect : RoutingEffect
    {
        public const string GroupName = "BigCart";
        public const string Name = nameof(SafeAreaEffect);
        public static readonly BindableProperty SafeAreaProperty = BindableProperty.CreateAttached("SafeArea", typeof(Sides), typeof(SafeAreaEffect), Sides.None, propertyChanged: OnSafeAreaChanged);
        public static readonly BindableProperty MarginProperty = BindableProperty.CreateAttached($"{nameof(SafeAreaEffect)}.Margin", typeof(Thickness), typeof(SafeAreaEffect), default(Thickness));

        public SafeAreaEffect() : base($"{GroupName}.{Name}")
        {
        }

        public static Sides GetSafeArea(BindableObject view)
        {
            return (Sides)view.GetValue(SafeAreaProperty);
        }

        public static Thickness GetMargin(BindableObject view)
        {
            return (Thickness)view.GetValue(MarginProperty);
        }

        public static void SetSafeArea(BindableObject view, Sides value)
        {
            view.SetValue(SafeAreaProperty, value);
        }

        public static void SetMargin(BindableObject view, Thickness value)
        {
            view.SetValue(MarginProperty, value);
        }

        private static void OnSafeAreaChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view)
            {
                Sides safeArea = (Sides)newValue;
                if (safeArea != Sides.None)
                {
                    var effect = view.Effects.FirstOrDefault(e => e is SafeAreaEffect);
                    if (effect == null)
                        view.Effects.Add(new SafeAreaEffect());
                }
                else
                {
                    var effect = view.Effects.FirstOrDefault(e => e is SafeAreaEffect);
                    if (effect != null)
                        view.Effects.Remove(effect);
                }
            }
        }
    }
}
