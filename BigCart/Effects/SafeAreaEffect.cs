using System.Linq;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace BigCart.Effects
{
    public class SafeAreaEffect : RoutingEffect
    {
        public const string GroupName = "BigCart";
        public const string Name = nameof(SafeAreaEffect); 
        public static readonly BindableProperty SafeAreaProperty = BindableProperty.CreateAttached("SafeArea", typeof(SafeArea), typeof(SafeAreaEffect), default(SafeArea), propertyChanged: OnSafeAreaChanged);

        public SafeAreaEffect() : base($"{GroupName}.{Name}")
        {
        }

        public static SafeArea GetSafeArea(BindableObject view)
        => (SafeArea)view.GetValue(SafeAreaProperty);

        public static void SetSafeArea(BindableObject view, SafeArea value)
            => view.SetValue(SafeAreaProperty, value);

        private static void OnSafeAreaChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view)
            {
                Effect oldEffect = view.Effects.FirstOrDefault(e => e is SafeAreaEffect);

                if (oldEffect != null)
                    view.Effects.Remove(oldEffect);

                if (((SafeArea)newValue).IsEmpty)
                    return;

                view.Effects.Add(new SafeAreaEffect());
            }
        }
    }
}
