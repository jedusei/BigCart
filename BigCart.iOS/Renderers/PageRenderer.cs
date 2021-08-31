using BigCart.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Page = BigCart.Pages.Page;

[assembly: ExportRenderer(typeof(Page), typeof(BigCart.iOS.Renderers.PageRenderer))]
namespace BigCart.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            if (e.NewElement != null)
                e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UpdateStatusBarStyle();
        }

        void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Page.StatusBarStyleProperty.PropertyName)
                UpdateStatusBarStyle();
        }

        void UpdateStatusBarStyle()
        {
            UIStatusBarStyle statusBarStyle = Element.GetValue(Page.StatusBarStyleProperty) switch
            {
                StatusBarStyle.LightContent => UIStatusBarStyle.LightContent,
                StatusBarStyle.DarkContent => UIStatusBarStyle.DarkContent,
                _ => UIStatusBarStyle.Default
            };

            if (statusBarStyle != UIApplication.SharedApplication.StatusBarStyle)
                UIApplication.SharedApplication.SetStatusBarStyle(statusBarStyle, true);
        }
    }
}