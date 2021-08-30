using BigCart.Effects;
using BigCart.iOS.Effects;
using UIKit;
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
            Element.PropertyChanged += ElementPropertyChanged;
            Apply();
        }

        protected override void OnDetached()
        {
            Element.PropertyChanged -= ElementPropertyChanged;
            Element.Margin = 0;
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SafeAreaEffect.SafeAreaProperty.PropertyName || e.PropertyName == SafeAreaEffect.MarginProperty.PropertyName)
                Apply();
        }

        private void Apply()
        {
            UIEdgeInsets insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
            Sides safeArea = SafeAreaEffect.GetSafeArea(Element);
            if (safeArea != Sides.None)
            {
                Thickness margin = SafeAreaEffect.GetMargin(Element);
                Element.Margin = new Thickness(
                    margin.Left + CalculateInsets(insets.Left, safeArea, Sides.Left),
                    margin.Top + CalculateInsets(insets.Top, safeArea, Sides.Top),
                    margin.Right + CalculateInsets(insets.Right, safeArea, Sides.Right),
                    margin.Bottom + CalculateInsets(insets.Bottom, safeArea, Sides.Bottom)
                );
            }
        }

        private static double CalculateInsets(double insetsComponent, Sides safeArea, Sides flag)
        {
            return safeArea.HasFlag(flag) ? insetsComponent : 0;
        }
    }
}