using Xamarin.Forms;

namespace BigCart.Controls
{
    public class ScrollViewEx : ScrollView
    {
        public static readonly BindableProperty IsScrollEnabledProperty = BindableProperty.Create(nameof(IsScrollEnabledProperty), typeof(bool), typeof(ScrollViewEx), true);

        public bool IsScrollEnabled
        {
            get => (bool)GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }
    }
}
