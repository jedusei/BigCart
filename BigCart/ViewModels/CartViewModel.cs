using BigCart.Controls;
using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Cart;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class CartViewModel : ViewModel
    {
        private readonly ICartService _cartService;
        private LayoutState _loadState = LayoutState.Loading;
        private bool _isWatchingCollection;
        private float _subtotal;
        private float _shippingCost;
        private float _total;

        public LayoutState LoadState
        {
            get => _loadState;
            private set => SetProperty(ref _loadState, value);
        }
        public ReadOnlyObservableCollection<Product> Items { get; private set; }
        public float Subtotal
        {
            get => _subtotal;
            private set => SetProperty(ref _subtotal, value);
        }
        public float ShippingCost
        {
            get => _shippingCost;
            private set => SetProperty(ref _shippingCost, value);
        }
        public float Total
        {
            get => _total;
            private set => SetProperty(ref _total, value);
        }
        public IAsyncCommand ClearCartCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand UpdateCostsCommand { get; }
        public ICommand CheckoutCommand { get; }

        public CartViewModel(ICartService cartService)
        {
            _cartService = cartService;
            ClearCartCommand = new AsyncCommand(ClearCartAsync, _ => Items?.Count > 0);
            RemoveItemCommand = new AsyncCommand<Product>(RemoveItemAsync);
            UpdateCostsCommand = new Command(UpdateCosts);
            CheckoutCommand = new AsyncCommand(() => _navigationService.PushAsync<CheckoutPage>(), allowsMultipleExecutions: false);
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = LoadItemsAsync();
        }

        public override void OnStop()
        {
            base.OnStop();
            if (_isWatchingCollection)
            {
                ((INotifyCollectionChanged)Items).CollectionChanged -= OnCartChanged;
                _isWatchingCollection = false;
            }
        }

        private async Task LoadItemsAsync()
        {
            LoadState = LayoutState.Loading;

            Items = await _cartService.GetItemsAsync();
            OnPropertyChanged(nameof(Items));
            OnCartChanged(this, null);

            if (!_isWatchingCollection)
            {
                ((INotifyCollectionChanged)Items).CollectionChanged += OnCartChanged;
                _isWatchingCollection = true;
            }
        }

        private async Task ClearCartAsync()
        {
            bool confirmed = await _modalService.ConfirmAsync("Are you sure you want to remove all items from the cart?", "Clear Cart");
            if (!confirmed)
                return;

            _modalService.ShowLoading("Clearing cart...");

            await _cartService.ClearCartAsync();

            _modalService.HideLoading();
        }

        private async Task RemoveItemAsync(Product product)
        {
            bool confirmed = await _modalService.ConfirmAsync("Are you sure you want to remove this item from the cart?");
            if (confirmed)
                _cartService.SetCartStatus(product, false);

            SwipeViewEx.Close(product);
        }

        private void OnCartChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCosts();
            LoadState = (Items.Count > 0) ? LayoutState.Success : LayoutState.Empty;
            ClearCartCommand.RaiseCanExecuteChanged();
        }

        private void UpdateCosts()
        {
            Subtotal = Items.Sum(p => p.Price * p.Quantity);
            ShippingCost = Items.Sum(p => p.Quantity) * AppConsts.UNIT_SHIPPING_COST;
            Total = _subtotal + _shippingCost;
        }
    }
}
