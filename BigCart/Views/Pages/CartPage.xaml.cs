using BigCart.Models;
using BigCart.ViewModels;
using Syncfusion.XForms.EffectsView;

namespace BigCart.Pages
{
    public partial class CartPage : Page
    {
        private CartViewModel _viewModel;

        public CartPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = BindingContext as CartViewModel;
        }

        private void IncreaseButtonClicked(object sender, System.EventArgs e)
        {
            var product = (Product)(sender as SfEffectsView).TouchUpCommandParameter;
            ChangeQuantity(product, 1);
        }

        private void DecreaseButtonClicked(object sender, System.EventArgs e)
        {
            var product = (Product)(sender as SfEffectsView).TouchUpCommandParameter;
            ChangeQuantity(product, -1);
        }

        private void ChangeQuantity(Product product, int delta)
        {
            if (delta > 0 || product.Quantity > 1)
            {
                product.Quantity += delta;
                _viewModel.UpdateCostsCommand.Execute(null);
            }
        }
    }
}