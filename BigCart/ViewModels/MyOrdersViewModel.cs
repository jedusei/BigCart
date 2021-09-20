using BigCart.Models;
using BigCart.Services.Orders;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace BigCart.ViewModels
{
    public class MyOrdersViewModel : ViewModel
    {
        private readonly IOrderService _orderService;
        private LayoutState _loadState;

        public LayoutState LoadState
        {
            get => _loadState;
            private set => SetProperty(ref _loadState, value);
        }
        public Order[] Orders { get; private set; }

        public MyOrdersViewModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            LoadState = LayoutState.Loading;

            Orders = await _orderService.GetOrdersAsync();
            foreach (Order order in Orders)
                order.IsExpanded = false;

            OnPropertyChanged(nameof(Orders));

            LoadState = (Orders.Length > 0) ? LayoutState.Success : LayoutState.Empty;
        }
    }
}
