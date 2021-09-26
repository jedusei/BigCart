using BigCart.DependencyInjection;
using BigCart.Models;
using System.Threading.Tasks;

namespace BigCart.Services.Orders
{
    public interface IOrderService : IDependency
    {
        Order LatestOrder { get; }
        Task<Order> CreateOrderAsync(CreateOrderInput input);
        Task<Order[]> GetOrdersAsync();
    }
}
