using Android.Content;
using BigCart.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScrollViewEx), typeof(BigCart.Droid.Renderers.ScrollViewExRenderer))]
namespace BigCart.Droid.Renderers
{
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        public ScrollViewExRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= ElementPropertyChanged;

            if (e.NewElement != null)
            {
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
            Element.IsEnabled = (Element as ScrollViewEx).IsScrollEnabled;
        }

        private void UpdateIsBounceEnabled()
        {
            OverScrollMode = (Element as ScrollViewEx).IsBounceEnabled
                ? Android.Views.OverScrollMode.IfContentScrolls
                : Android.Views.OverScrollMode.Never;
        }
    }
}