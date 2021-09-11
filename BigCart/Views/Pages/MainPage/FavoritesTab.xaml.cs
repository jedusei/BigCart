using BigCart.Models;
using Syncfusion.XForms.EffectsView;

namespace BigCart.Pages
{
    public partial class FavoritesTab : Tab
    {
        public FavoritesTab()
        {
            InitializeComponent();
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
                product.Quantity += delta;
        }
    }
}