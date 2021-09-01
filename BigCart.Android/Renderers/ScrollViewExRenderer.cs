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
                e.NewElement.PropertyChanged += ElementPropertyChanged;
            }
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ScrollViewEx.IsScrollEnabledProperty.PropertyName)
                UpdateIsScrollEnabled();
        }

        private void UpdateIsScrollEnabled()
        {
            Element.IsEnabled = (Element as ScrollViewEx).IsScrollEnabled;
        }
    }
}