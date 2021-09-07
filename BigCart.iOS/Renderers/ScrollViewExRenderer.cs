using BigCart.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollViewEx), typeof(BigCart.iOS.Renderers.ScrollViewExRenderer))]
namespace BigCart.iOS.Renderers
{
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= ElementPropertyChanged;

            if (e.NewElement != null)
            {
                ContentInsetAdjustmentBehavior = UIKit.UIScrollViewContentInsetAdjustmentBehavior.Never;
                UpdateIsScrollEnabled();
                UpdateIsBounceEnabled();
                e.NewElement.PropertyChanged += ElementPropertyChanged;
            }
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ScrollViewEx.IsScrollEnabledProperty.PropertyName)
                UpdateIsScrollEnabled();
            else if (e.PropertyName == ScrollViewEx.IsBounceEnabledProperty.PropertyName)
                UpdateIsBounceEnabled();

        }

        private void UpdateIsScrollEnabled()
        {
            ScrollEnabled = (Element as ScrollViewEx).IsScrollEnabled;
        }

        private void UpdateIsBounceEnabled()
        {
            Bounces = (Element as ScrollViewEx).IsBounceEnabled;
        }
    }
}