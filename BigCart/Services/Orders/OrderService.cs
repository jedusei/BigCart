using BigCart.Models;
using BigCart.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigCart.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private List<Order> _orders = new();
        private static int _nextItemId = new Random().Next(10000, 99999);

        public OrderService(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<Order[]> GetOrdersAsync()
        {
            await Task.Delay(1000);
            return _orders.ToArray();
        }

        public async Task<Order> PlaceOrderAsync()
        {
            var delayTask = Task.Delay(1000);

            var items = await _cartService.GetItemsAsync();
            int quantity = items.Sum(i => i.Quantity);
            float cost = items.Sum(i => i.Quantity * i.Price) + (quantity * AppConsts.UNIT_SHIPPING_COST);

            await _cartService.ClearCartAsync();

            Order order = new(_nextItemId++, quantity, cost);
            _orders.Add(order);

            await delayTask;

            return order;
        }
    }
}
