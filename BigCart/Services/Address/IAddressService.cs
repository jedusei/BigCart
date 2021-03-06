using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AddressModel = BigCart.Models.Address;

namespace BigCart.Services.Address
{
    public interface IAddressService
    {
        Task<ReadOnlyObservableCollection<AddressModel>> GetAddressesAsync();
        Task<AddressModel> AddAddressAsync(AddAddressInput input);
        Task UpdateAddressAsync(AddressModel updatedAddress);
        Task<AddressModel> GetDefaultAddressAsync();
        Task SetDefaultAddressAsync(int addressId);
    }
}
