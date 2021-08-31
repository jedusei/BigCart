using Android.Content;
using Android.Runtime;
using Android.Views;
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
        public PageRenderer(Context context) : base(context)
        {
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

        void UpdateStatusBarStyle()
        {
            var statusBarStyle = (StatusBarStyle)Element.GetValue(Page.StatusBarStyleProperty);

#pragma warning disable CS0618 // Type or member is obsolete
            if (statusBarStyle == StatusBarStyle.DarkContent)
                Context.GetActivity().Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            else
                Context.GetActivity().Window.DecorView.SystemUiVisibility &= (StatusBarVisibility)~SystemUiFlags.LightStatusBar;
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}