using BigCart.DependencyInjection;
using BigCart.Models;
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
                Price = 8,
                Weight = 12,
                Color = Color.FromHex("#FFCEC1"),
                ImageSource = ImageSource.FromFile("peach.png")
            },
            new()
            {
                Name = "Avocado",
                Price = 7,
                Weight = 2,
                IsInCart = true,
                Quantity = 1,
                Color = Color.FromHex("#FCFFD9"),
                ImageSource = ImageSource.FromFile("avocado.png")
            },
            new()
            {
                Name = "Pineapple",
                Price = 9.9f,
                Weight = 1.5f,
                IsFavorite = true,
                Color = Color.FromHex("#FFE6C2"),
                ImageSource = ImageSource.FromFile("pineapple.png")
            },
            new()
            {
                Name = "Black Grapes",
                Price = 7.05f,
                Discount = 16,
                Weight = 5,
                Color = Color.FromHex("#FEE1ED"),
                ImageSource = ImageSource.FromFile("grapes.png")
            },
            new()
            {
                Name = "Pomegranate",
                Price = 2.09f,
                Weight = 1.5f,
                IsInCart = true,
                Quantity = 1,
                Color = Color.FromHex("#FFE3E2"),
                ImageSource = ImageSource.FromFile("pomegranate.png")
            },
            new()
            {
                Name = "Fresh Broccoli",
                Price = 3f,
                Weight = 2.2f,
                IsFavorite = true,
                Color = Color.FromHex("#D2FFD0"),
                ImageSource = ImageSource.FromFile("broccoli.png")
            },
        };

        public async Task<Product[]> GetProductsAsync()
        {
            await Task.Delay(1000);
            return _products;
        }
    }
}
