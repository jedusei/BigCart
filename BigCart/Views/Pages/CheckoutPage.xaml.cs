using BigCart.ViewModels;
using Syncfusion.XForms.ProgressBar;

namespace BigCart.Pages
{
    public partial class CheckoutPage : Page
    {
        CheckoutViewModel _viewModel;

        public CheckoutPage()
        {
            InitializeComponent();
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