using BigCart.DependencyInjection;
using BigCart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart.Services.Products
{
    public class ProductService : IProductService, ISingletonDependency
    {
        private static Product[] _products = new Product[] {
            new()
            {
                Name = "Fresh Peach",
                Category = "Fruits",
                Price = 8,
                Weight = 12,
                Rating = 4.5f,
                Color = Color.FromHex("#FFCEC1"),
                ImageSource = ImageSource.FromFile("peach.png")
            },
            new()
            {
                Name = "Avocado",
                Category = "Fruits",
                Price = 7,
                Weight = 2,
                Rating = 3,
                IsInCart = true,
                Quantity = 1,
                Color = Color.FromHex("#FCFFD9"),
                ImageSource = ImageSource.FromFile("avocado.png")
            },
            new()
            {
                Name = "Pineapple",
                Category = "Fruits",
                Price = 9.9f,
                Weight = 1.5f,
                Rating = 4,
                IsFavorite = true,
                Color = Color.FromHex("#FFE6C2"),
                ImageSource = ImageSource.FromFile("pineapple.png")
            },
            new()
            {
                Name = "Black Grapes",
                Category = "Fruits",
                Price = 7.05f,
                Discount = 16,
                Weight = 5,
                Rating = 4.5f,
                Color = Color.FromHex("#FEE1ED"),
                ImageSource = ImageSource.FromFile("grapes.png")
            },
            new()
            {
                Name = "Pomegranate",
                Category = "Fruits",
                Price = 2.09f,
                Weight = 1.5f,
                Rating = 3,
                IsInCart = true,
                Quantity = 1,
                Color = Color.FromHex("#FFE3E2"),
                ImageSource = ImageSource.FromFile("pomegranate.png")
            },
            new()
            {
                Name = "Fresh Broccoli",
                Category = "Fruits",
                Price = 3f,
                Weight = 2.2f,
                Rating = 2.5f,
                IsFavorite = true,
                Color = Color.FromHex("#D2FFD0"),
                ImageSource = ImageSource.FromFile("broccoli.png")
            },
        };
        private readonly ObservableCollection<Product> _favorites;
        private readonly ReadOnlyObservableCollection<Product> _favoritesReadonly;
        private readonly ObservableCollection<string> _searchHistory;

        public ProductService()
        {
            _favorites = new ObservableCollection<Product>(_products.Where(p => p.IsFavorite));
            _favoritesReadonly = new ReadOnlyObservableCollection<Product>(_favorites);
            _searchHistory = new ObservableCollection<string>();
        }

        public async Task<Product[]> GetProductsAsync(ProductFilter filter = null)
        {
            await Task.Delay(1000);
            IEnumerable<Product> results = _products;

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    results = results.Where(p => p.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));

                    // Update search history
                    string historyEntry = _searchHistory.FirstOrDefault(s => s.Equals(filter.Name, StringComparison.OrdinalIgnoreCase));
                    if (historyEntry == null)
                        _searchHistory.Insert(0, filter.Name);
                    else
                        _searchHistory.Move(_searchHistory.IndexOf(historyEntry), 0);
                }

                if (!string.IsNullOrWhiteSpace(filter.Category))
                    results = results.Where(p => filter.Category.Equals(p.Category, StringComparison.OrdinalIgnoreCase));

                if (filter.MinPrice.HasValue)
                    results = results.Where(p => p.Price >= filter.MinPrice);

                if (filter.MaxPrice.HasValue)
                    results = results.Where(p => p.Price <= filter.MaxPrice);

                if (filter.Rating != 0)
                    results = results.Where(p => p.Rating >= filter.Rating);

                if (filter.Discount)
                    results = results.Where(p => p.Discount > 0);
            }

            return results.ToArray();
        }

        public void SetFavoriteStatus(Product product, bool isFavorite)
        {
            if (isFavorite && !_favorites.Contains(product))
                _favorites.Add(product);
            else if (!isFavorite && _favorites.Contains(product))
                _favorites.Remove(product);

            product.IsFavorite = isFavorite;
        }

        public ReadOnlyObservableCollection<Product> GetFavoriteProducts()
        {
            return _favoritesReadonly;
        }

        public ObservableCollection<string> GetSearchHistory()
        {
            return _searchHistory;
        }
    }
}
