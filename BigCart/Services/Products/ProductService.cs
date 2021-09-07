using BigCart.DependencyInjection;
using BigCart.Models;
using System;
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

        public async Task<Product[]> GetProductsAsync(ProductFilter filter = null)
        {
            await Task.Delay(1000);
            return string.IsNullOrWhiteSpace(filter?.Category)
                ? _products
                : _products.Where(p => filter.Category.Equals(p.Category, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
        }
    }
}
