using BigCart.DependencyInjection;
using BigCart.Models;
using BigCart.Services.Products;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BigCart.Services.Cart
{
    public class CartService : ICartService, ISingletonDependency
    {
        private readonly IProductService _productService;
        private ObservableCollection<Product> _items;
        private ReadOnlyObservableCollection<Product> _itemsReadonly;

        public CartService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ReadOnlyObservableCollection<Product>> GetItemsAsync()
        {
            var delay = Task.Delay(1000);

            if (_items == null)
            {
                Product[] products = await _productService.GetProductsAsync();
                _items = new ObservableCollection<Product>(products.Where(p => p.IsInCart));
                _itemsReadonly = new ReadOnlyObservableCollection<Product>(_items);
            }

            await delay;
            return _itemsReadonly;
        }

        public void SetCartStatus(Product product, bool isInCart)
        {
            if (!isInCart)
                _items.Remove(product);
            else if (!_items.Contains(product))
                _items.Add(product);

            product.IsInCart = isInCart;
            product.Quantity = isInCart ? 1 : 0;
        }
    }
}
