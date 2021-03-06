using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Cart;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class ProductViewModel : ViewModel
    {
        public Product Product { get; private set; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ShowReviewsCommand { get; }
        public ICommand ChangeQuantityCommand { get; }
        public ICommand AddToCartCommand { get; }

        public ProductViewModel(ICartService cartService)
        {
            ToggleFavoriteCommand = new Command(() => Product.IsFavorite = !Product.IsFavorite);
            ShowReviewsCommand = new AsyncCommand(() => _navigationService.PushAsync<ReviewsPage>(new() { Data = Product }));
            ChangeQuantityCommand = new Command<int>(delta =>
            {
                if (delta > 0 || Product.Quantity > 1)
                    Product.Quantity += delta;
            });
            AddToCartCommand = new Command(() => cartService.SetCartStatus(Product, true));
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            Product = (Product)navigationData;
            OnPropertyChanged(nameof(Product));
        }
    }
}
