using BigCart.Models;
using BigCart.Services.Address;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class AddAddressViewModel : ModalViewModel
    {
        private readonly IAddressService _addressService;

        public AddAddressInput Address { get; } = new();
        public IAsyncCommand SaveCommand { get; }

        public AddAddressViewModel(IAddressService addressService)
        {
            _addressService = addressService;
            SaveCommand = new AsyncCommand(SaveAddressAsync);
        }

        public async Task SaveAddressAsync()
        {
            _modalService.ShowLoading("Saving address...");

            Address address = await _addressService.AddAddressAsync(Address);

            _modalService.HideLoading();

            SetResult(address);
        }
    }
}
