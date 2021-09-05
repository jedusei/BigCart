using BigCart.DependencyInjection;
using BigCart.Models;
using System.Threading.Tasks;

namespace BigCart.Services.Products
{
    public interface IProductService : IDependency
    {
        Task<Product[]> GetProductsAsync();
    }
}
