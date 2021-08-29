using Android.App;
using Android.Views;
using BigCart.Droid.Effects;
using BigCart.Effects;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ResolutionGroupName(SafeAreaEffect.GroupName)]
[assembly: ExportEffect(typeof(SafeAreaEffectRouter), SafeAreaEffect.Name)]
namespace BigCart.Droid.Effects
{
    public class SafeAreaEffectRouter : PlatformEffect
    {
        public new View Element => (View)base.Element;

        protected override void OnAttached()
        {
            WindowInsets insets = Container.Context.GetActivity().Window.DecorView.RootWindowInsets;
            SafeArea safeArea = SafeAreaEffect.GetSafeArea(Element);

#pragma warning disable CS0618 // Type or member is obsolete
            Element.Margin = new Thickness(
                CalculateInsets(insets.StableInsetLeft, safeArea.Left),
                CalculateInsets(insets.StableInsetTop, safeArea.Top),
                CalculateInsets(insets.StableInsetRight, safeArea.Right),
                CalculateInsets(insets.StableInsetBottom, safeArea.Bottom)
            );
#pragma warning restore CS0618 // Type or member is obsolete
        }

        protected override void OnDetached()
        {
            Element.Margin = 0;
        }

        private double CalculateInsets(double insetsComponent, bool shouldUseInsetsComponent)
        {
            return shouldUseInsetsComponent ? insetsComponent : 0;
        }
    }
}