using BigCart.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public partial class ProductView : ContentView
    {
        public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(Product), typeof(ProductView));
        public static readonly BindableProperty ToggleFavoriteCommandProperty = BindableProperty.Create(nameof(ToggleFavoriteCommand), typeof(ICommand), typeof(ProductView));
        public static readonly BindableProperty ViewCommandProperty = BindableProperty.Create(nameof(ViewCommand), typeof(ICommand), typeof(ProductView));
        public static readonly BindableProperty AddToCartCommandProperty = BindableProperty.Create(nameof(AddToCartCommand), typeof(ICommand), typeof(ProductView));

        public Product Product
        {
            get => (Product)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }
        public ICommand ToggleFavoriteCommand
        {
            get => (ICommand)GetValue(ToggleFavoriteCommandProperty);
            set => SetValue(ToggleFavoriteCommandProperty, value);
        }
        public ICommand ViewCommand
        {
            get => (ICommand)GetValue(ViewCommandProperty);
            set => SetValue(ViewCommandProperty, value);
        }
        public ICommand AddToCartCommand
        {
            get => (ICommand)GetValue(AddToCartCommandProperty);
            set => SetValue(AddToCartCommandProperty, value);
        }

        public ProductView()
        {
            InitializeComponent();
        }

        private void DecreaseQuantityButtonClicked(object sender, System.EventArgs e)
        {
            if (Product.Quantity > 1)
                Product.Quantity--;
        }

        private void IncreaseQuantityButtonClicked(object sender, System.EventArgs e)
        {
            Product.Quantity++;
        }
    }
}