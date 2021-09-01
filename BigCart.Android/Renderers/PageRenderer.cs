using Android.Content;
using Android.Runtime;
using Android.Views;
using AndroidX.Core.View;
using BigCart.Pages;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Page = BigCart.Pages.Page;

[assembly: ExportRenderer(typeof(Page), typeof(BigCart.Droid.Renderers.PageRenderer))]
namespace BigCart.Droid.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        private Window _window;
        private static double _navBarHeight;

        public PageRenderer(Context context) : base(context)
        {
            _window = context.GetActivity().Window;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Page.StatusBarStyleProperty.PropertyName)
                UpdateStatusBarStyle();
        }

        protected override void OnVisibilityChanged(Android.Views.View changedView, [GeneratedEnum] ViewStates visibility)
        {
            base.OnVisibilityChanged(changedView, visibility);
            if (visibility == ViewStates.Visible)
                UpdateStatusBarStyle();
        }

        public override WindowInsets OnApplyWindowInsets(WindowInsets insets)
        {
            WindowInsetsCompat insetsCompat = WindowInsetsCompat.ToWindowInsetsCompat(insets);

            if (_window.Attributes.SoftInputMode != SoftInput.AdjustResize)
                _navBarHeight = insetsCompat.GetInsets(WindowInsetsCompat.Type.NavigationBars()).Bottom;
            else if (IsShown)
            {
                double keyboardHeight = insetsCompat.GetInsets(WindowInsetsCompat.Type.Ime()).Bottom;
                keyboardHeight -= _navBarHeight;
                Thickness padding = Element.Padding;
                padding.Bottom = (keyboardHeight > 0) ? Context.FromPixels(keyboardHeight) : 0;
                Element.Padding = padding;
            }

            return base.OnApplyWindowInsets(insets);
        }

        void UpdateStatusBarStyle()
        {
            var statusBarStyle = (StatusBarStyle)Element.GetValue(Page.StatusBarStyleProperty);

#pragma warning disable CS0618 // Type or member is obsolete
            if (statusBarStyle == StatusBarStyle.DarkContent)
                _window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            else
                _window.DecorView.SystemUiVisibility &= (StatusBarVisibility)~SystemUiFlags.LightStatusBar;
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}