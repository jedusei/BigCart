using BigCart.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AddressModel = BigCart.Models.Address;

namespace BigCart.Services.Address
{
    public interface IAddressService : IDependency
    {
        Task<ReadOnlyObservableCollection<AddressModel>> GetAddressesAsync();
        Task AddAddressAsync(AddressModel address);
        Task UpdateAddressAsync(AddressModel updatedAddress);
        Task<AddressModel> GetDefaultAddressAsync();
        Task SetDefaultAddressAsync(int addressId);
    }
}
