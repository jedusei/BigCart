using BigCart.Controls;
using BigCart.Models;
using BigCart.Services.Products;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class FavoritesViewModel : ViewModel
    {
        public ReadOnlyObservableCollection<Product> Favorites { get; }
        public ICommand RemoveItemCommand { get; }

        public FavoritesViewModel(IProductService productService)
        {
            Favorites = productService.GetFavoriteProducts();
            RemoveItemCommand = new AsyncCommand<Product>(async (product) =>
            {
                bool confirmed = await _modalService.ConfirmAsync("Are you sure you want to remove this item from your favorites?");
                if (confirmed)
                    productService.SetFavoriteStatus(product, false);

                SwipeViewEx.Close(product);
            });
        }
    }
}
