using BigCart.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AddressModel = BigCart.Models.Address;

namespace BigCart.Services.Address
{
    public class AddressService : IAddressService, ISingletonDependency
    {
        private readonly ObservableCollection<AddressModel> _addresses;
        private readonly ReadOnlyObservableCollection<AddressModel> _addressesReadOnly;

        public AddressService()
        {
            _addresses = new()
            {
                new AddressModel
                {
                    Name = "Russell Austin",
                    PhoneNumber = "+1 202 555 0142",
                    Value = "2811 Crescent Day LA Port",
                    City = "California",
                    ZipCode = "77571",
                    Country = "United States",
                    IsDefault = true
                },
                new AddressModel
                {
                    Name = "Jessica Simpson",
                    PhoneNumber = "+1 202 555 0142",
                    Value = "2811 Crescent Day LA Port",
                    City = "California",
                    ZipCode = "77571",
                    Country = "United States",
                    IsDefault = true
                }
            };

            _addressesReadOnly = new(_addresses);
        }

        public async Task AddAddressAsync(AddressModel address)
        {
            await Task.Delay(1000);
            if (!_addresses.Contains(address))
                _addresses.Add(address);
        }

        public async Task<ReadOnlyObservableCollection<AddressModel>> GetAddressesAsync()
        {
            await Task.Delay(1000);
            return _addressesReadOnly;
        }

        public async Task<AddressModel> GetDefaultAddressAsync()
        {
            await Task.Delay(1000);
            return _addresses.FirstOrDefault(a => a.IsDefault);
        }

        public async Task SetDefaultAddressAsync(int addressId)
        {
            await Task.Delay(1000);
            AddressModel a = _addresses.FirstOrDefault(b => b.Id == addressId);

            if (a != null)
            {
                foreach (AddressModel x in _addresses)
                    x.IsDefault = x == a;
            }
        }

        public async Task UpdateAddressAsync(AddressModel updatedAddress)
        {
            await Task.Delay(1000);
            AddressModel address = _addresses.FirstOrDefault(a => a.Id == updatedAddress.Id);
            address?.CopyFrom(updatedAddress);
        }
    }
}
