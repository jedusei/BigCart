using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public partial class VerificationCodeView : ContentView
    {
        public static readonly BindableProperty LengthProperty = BindableProperty.Create(nameof(Length), typeof(int), typeof(VerificationCodeView), AppConsts.OTP_LENGTH);
        public static readonly BindableProperty CodeProperty = BindableProperty.Create(nameof(Code), typeof(string), typeof(VerificationCodeView), defaultBindingMode: BindingMode.TwoWay);

        public int Length
        {
            get => (int)GetValue(LengthProperty);
            set => SetValue(LengthProperty, value);
        }
        public string Code
        {
            get => (string)GetValue(CodeProperty);
            set => SetValue(CodeProperty, value);
        }

        public VerificationCodeView()
        {
            InitializeComponent();
            UpdateBindableLayoutItemSource();
        }

        public new bool Focus()
        {
            return _entry.Focus();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == LengthProperty.PropertyName)
                UpdateBindableLayoutItemSource();
        }

        private void UpdateBindableLayoutItemSource()
        {
            BindableLayout.SetItemsSource(_codeViewLayout, Enumerable.Range(0, Length));
        }

        private void CharacterView_Loaded(object sender, System.EventArgs e)
        {
            var view = (Layout)sender;
            int index = (int)view.BindingContext;
            var label = (Label)view.Children[0];
            label.SetBinding(Label.TextProperty, new Binding()
            {
                Path = $"{nameof(Code)}[{index}]",
                Source = this,
                FallbackValue = "•"
            });
        }

        private void CharacterView_Tapped(object sender, System.EventArgs e)
        {
            Focus();
        }
    }
}