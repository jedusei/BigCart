using BigCart.DependencyInjection;
using BigCart.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BigCart.Services.Products
{
    public interface IProductService : IDependency
    {
        Task<Product[]> GetProductsAsync(ProductFilter filter = null);
        void SetFavoriteStatus(Product product, bool isFavorite);
        ReadOnlyObservableCollection<Product> GetFavoriteProducts();
        ObservableCollection<string> GetSearchHistory();
    }
}
