using BigCart.DependencyInjection;
using BigCart.Models;
using BigCart.Services.Cart;
using BigCart.Services.Transactions;
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
        private List<Order> _orders = new();

        public Order LatestOrder { get; private set; }

        public OrderService(ICartService cartService, ITransactionService transactionService)
        {
            _cartService = cartService;
            _transactionService = transactionService;
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

            Transaction transaction = new()
            {
                Amount = cost,
                PaymentMethod = input.PaymentMethod,
                CreditCardType = input.CreditCardType
            };
            await _transactionService.RegisterTransactionAsync(transaction);

            await _cartService.ClearCartAsync();

            Order order = new(_nextOrderId++, quantity, cost);
            _orders.Insert(0, order);
            LatestOrder = order;

            await delayTask;

            return order;
        }
    }
}
