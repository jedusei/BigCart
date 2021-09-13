using BigCart.ViewModels;
using Syncfusion.XForms.ProgressBar;
using Xamarin.Forms;

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