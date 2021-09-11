using BigCart.Models;
using BigCart.Services.Products;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class FavoritesViewModel : ViewModel
    {
        public ReadOnlyObservableCollection<Product> Favorites { get; }
        public ICommand RemoveItemCommand { get; }

        public FavoritesViewModel(IProductService productService)
        {
            Favorites = productService.GetFavoriteProducts();
            RemoveItemCommand = new Command<Product>(p => productService.SetFavoriteStatus(p, false));
        }
    }
}
