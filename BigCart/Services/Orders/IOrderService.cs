using BigCart.DependencyInjection;
using BigCart.Models;
using System.Threading.Tasks;

namespace BigCart.Services.Orders
{
    public interface IOrderService : IDependency
    {
        Task<Order> PlaceOrderAsync();
        Task<Order[]> GetOrdersAsync();
    }
}
