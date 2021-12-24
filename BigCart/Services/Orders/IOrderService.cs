using BigCart.Models;
using System.Threading.Tasks;

namespace BigCart.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderInput input);
        Task<Order[]> GetOrdersAsync();
    }
}
