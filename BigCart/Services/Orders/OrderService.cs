using BigCart.DependencyInjection;
using BigCart.Models;
using BigCart.Services.Cart;
using BigCart.Services.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigCart.Services.Orders
{
    public class OrderService : IOrderService, ISingletonDependency
    {
        private static int _nextOrderId = 90897;
        private readonly ICartService _cartService;
        private readonly ITransactionService _transactionService;
        private List<Order> _orders = new()
        {
            new()
            {
                Id = _nextOrderId++,
                ItemCount = 10,
                Cost = 16.9f,
                CreatedAt = DateTime.Today.AddDays(-1),
                ConfirmedAt = DateTime.Today,
                ShippedAt = DateTime.Today
            },
            new()
            {
                Id = _nextOrderId++,
                ItemCount = 10,
                Cost = 16.9f,
                CreatedAt = DateTime.Today.AddDays(-1),
                ConfirmedAt = DateTime.Today,
                ShippedAt = DateTime.Today
            },
            new()
            {
                Id = _nextOrderId++,
                ItemCount = 10,
                Cost = 16.9f,
                CreatedAt = DateTime.Today.AddDays(-1),
                ConfirmedAt = DateTime.Today,
                ShippedAt = DateTime.Today
            },
            new()
            {
                Id = _nextOrderId++,
                ItemCount = 10,
                Cost = 16.9f,
                CreatedAt = DateTime.Today.AddDays(-1),
                ConfirmedAt = DateTime.Today,
                ShippedAt = DateTime.Today
            }
        };

        public Order LatestOrder { get; private set; }

        public OrderService(ICartService cartService, ITransactionService transactionService)
        {
            _cartService = cartService;
            _transactionService = transactionService;
            _orders.Reverse();
        }

        public async Task<Order[]> GetOrdersAsync()
        {
            await Task.Delay(1000);
            return _orders.ToArray();
        }

        public async Task<Order> CreateOrderAsync(CreateOrderInput input)
        {
            var delayTask = Task.Delay(1000);

            var items = await _cartService.GetItemsAsync();
            int quantity = items.Sum(i => i.Quantity);
            float cost = items.Sum(i => i.Quantity * i.Price) + (quantity * AppConsts.UNIT_SHIPPING_COST);

            await _transactionService.CreateTransactionAsync(new(cost, input.PaymentMethod, input.CreditCard));
            await _cartService.ClearCartAsync();

            Order order = new()
            {
                Id = _nextOrderId++,
                ItemCount = quantity,
                Cost = cost
            };
            _orders.Insert(0, order);
            LatestOrder = order;

            await delayTask;

            return order;
        }
    }
}
