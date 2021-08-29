using BigCart.Effects;
using BigCart.iOS.Effects;
using UIKit;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName(SafeAreaEffect.GroupName)]
[assembly: ExportEffect(typeof(SafeAreaEffectRouter), SafeAreaEffect.Name)]
namespace BigCart.iOS.Effects
{
    public class SafeAreaEffectRouter : PlatformEffect
    {
        public new View Element => (View)base.Element;

        protected override void OnAttached()
        {
            UIEdgeInsets insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
            SafeArea safeArea = SafeAreaEffect.GetSafeArea(Element);

            Element.Margin = new Thickness(
                CalculateInsets(insets.Left, safeArea.Left),
                CalculateInsets(insets.Top, safeArea.Top),
                CalculateInsets(insets.Right, safeArea.Right),
                CalculateInsets(insets.Bottom, safeArea.Bottom)
            );
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