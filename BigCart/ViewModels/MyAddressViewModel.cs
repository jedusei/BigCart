using BigCart.Modals;
using BigCart.Models;
using BigCart.Services.Address;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace BigCart.ViewModels
{
    public class MyAddressViewModel : ViewModel
    {
        private readonly IAddressService _addressService;
        private LayoutState _loadState;

        public LayoutState LoadState
        {
            get => _loadState;
            set => SetProperty(ref _loadState, value);
        }
        public ReadOnlyObservableCollection<Address> Addresses { get; private set; }
        public IAsyncCommand AddAddressCommand { get; }
        public IAsyncCommand<Address> SetDefaultAddressCommand { get; }

        public MyAddressViewModel(IAddressService addressService)
        {
            _addressService = addressService;
            AddAddressCommand = new AsyncCommand(() => _modalService.PushAsync<AddAddressModal, Address>());
            SetDefaultAddressCommand = new AsyncCommand<Address>(SetDefaultAddressAsync);
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = GetAddressesAsync();
        }

        private async Task GetAddressesAsync()
        {
            LoadState = LayoutState.Loading;

            Addresses = await _addressService.GetAddressesAsync();
            OnPropertyChanged(nameof(Addresses));

            LoadState = Addresses.Count > 0 ? LayoutState.Success : LayoutState.Empty;
        }

        private async Task SetDefaultAddressAsync(Address address)
        {
            if (address.IsDefault)
                return;

            _modalService.ShowLoading("Setting default...");
            await _addressService.SetDefaultAddressAsync(address.Id);
            _modalService.HideLoading();
        }
    }
}
