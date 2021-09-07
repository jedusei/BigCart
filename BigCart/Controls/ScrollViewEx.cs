using Xamarin.Forms;

namespace BigCart.Controls
{
    public class ScrollViewEx : ScrollView
    {
        public static readonly BindableProperty IsScrollEnabledProperty = BindableProperty.Create(nameof(IsScrollEnabledProperty), typeof(bool), typeof(ScrollViewEx), true);
        public static readonly BindableProperty IsBounceEnabledProperty = BindableProperty.Create(nameof(IsBounceEnabled), typeof(bool), typeof(ScrollViewEx), true);

        public bool IsScrollEnabled
        {
            get => (bool)GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }
        public bool IsBounceEnabled
        {
            get => (bool)GetValue(IsBounceEnabledProperty);
            set => SetValue(IsBounceEnabledProperty, value);
        }
    }
}
