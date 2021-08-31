using Android.Content;
using Android.Views;
using BigCart.Droid.Effects;
using BigCart.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ResolutionGroupName(SafeAreaEffect.GroupName)]
[assembly: ExportEffect(typeof(SafeAreaEffectRouter), SafeAreaEffect.Name)]
namespace BigCart.Droid.Effects
{
    public class SafeAreaEffectRouter : PlatformEffect
    {
        private Context _context;
        public new View Element => (View)base.Element;

        protected override void OnAttached()
        {
            _context = (Container ?? Control).Context;
            Element.PropertyChanged += ElementPropertyChanged;
            Apply();
        }

        protected override void OnDetached()
        {
            Element.PropertyChanged -= ElementPropertyChanged;
            Element.Margin = 0;
            _context = null;
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SafeAreaEffect.SafeAreaProperty.PropertyName || e.PropertyName == SafeAreaEffect.MarginProperty.PropertyName)
                Apply();
        }

        private void Apply()
        {
            Sides safeArea = SafeAreaEffect.GetSafeArea(Element);
            if (safeArea != Sides.None)
            {
                Thickness margin = SafeAreaEffect.GetMargin(Element);
                WindowInsets insets = _context.GetActivity().Window.DecorView.RootWindowInsets;

#pragma warning disable CS0618 // Type or member is obsolete
                Element.Margin = new Thickness(
                    margin.Left + CalculateInsets(insets.StableInsetLeft, safeArea, Sides.Left),
                    margin.Top + CalculateInsets(insets.StableInsetTop, safeArea, Sides.Top),
                    margin.Right + CalculateInsets(insets.StableInsetRight, safeArea, Sides.Right),
                    margin.Bottom + CalculateInsets(insets.StableInsetBottom, safeArea, Sides.Bottom)
                );
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }

        private double CalculateInsets(double insetsComponent, Sides safeArea, Sides flag)
        {
            return safeArea.HasFlag(flag) ? _context.FromPixels(insetsComponent) : 0;
        }
    }
}