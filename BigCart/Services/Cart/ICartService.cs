using BigCart.DependencyInjection;
using BigCart.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BigCart.Services.Cart
{
    public interface ICartService : IDependency
    {
        Task<ReadOnlyObservableCollection<Product>> GetItemsAsync();
        void SetCartStatus(Product product, bool isInCart);
        Task ClearCartAsync();
    }
}
