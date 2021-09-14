using BigCart.ViewModels;
using Syncfusion.XForms.ProgressBar;
using Xamarin.Forms;
using AppIcons = BigCart.Icons.Icons;

namespace BigCart.Pages
{
    public partial class CheckoutPage : Page
    {
        CheckoutViewModel _viewModel;

        public CheckoutPage()
        {
            InitializeComponent();

            BindableLayout.SetItemsSource(_shippingMethodLayout, new[]
            {
                new
                {
                    Title = "Standard Delivery",
                    Description = "Order will be delivered between 3 - 4 business days straight to your doorstep.",
                    Price = 3
                },
                new
                {
                    Title = "Next Day Delivery",
                    Description = "Order will be delivered between 1 - 2 business days straight to your doorstep.",
                    Price = 5
                },
                new
                {
                    Title = "Nominated Delivery",
                    Description = "Order will be delivered on your chosen date.",
                    Price = 3
                }
            });

            string iconFontFamily = (string)Application.Current.Resources["Icons"];
            Color iconColor = (Color)Application.Current.Resources["TextSecondaryColor"];
            BindableLayout.SetItemsSource(_paymentMethodLayout, new[]
            {
                new
                {
                    Label = "Paypal",
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.Paypal,
                        Color = iconColor
                    }
                },
                new
                {
                    Label = "Credit Card",
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.CreditCard,
                        Color = iconColor
                    }
                },
                new
                {
                    Label = "Apple Pay",
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.AppleLogo,
                        Color = iconColor
                    }
                }
            });
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = BindingContext as CheckoutViewModel;
        }

        private void SfStepProgressBar_StepTapped(object sender, StepTappedEventArgs e)
        {
            if (e.Item.Status != StepStatus.NotStarted)
                _viewModel.CurrentStep = e.Index;
        }
    }
}