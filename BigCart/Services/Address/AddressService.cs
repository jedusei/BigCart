using BigCart.DependencyInjection;
using BigCart.Models;
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
        private static int _nextAddressId = 1;

        public AddressService()
        {
            Country country = Country.All.FirstOrDefault(c => c.NameCode == "USA");
            _addresses = new()
            {
                new AddressModel
                {
                    Id = _nextAddressId++,
                    Name = "Russell Austin",
                    PhoneNumber = "+1 202 555 0142",
                    Value = "2811 Crescent Day LA Port",
                    City = "California",
                    ZipCode = "77571",
                    Country = country,
                    IsDefault = true
                },
                new AddressModel
                {
                    Id = _nextAddressId++,
                    Name = "Jessica Simpson",
                    PhoneNumber = "+1 202 555 0142",
                    Value = "2811 Crescent Day LA Port",
                    City = "California",
                    ZipCode = "77571",
                    Country = country
                }
            };

            _addressesReadOnly = new(_addresses);
        }

        public async Task<AddressModel> AddAddressAsync(AddAddressInput input)
        {
            await Task.Delay(1000);

            AddressModel address = new()
            {
                Id = _nextAddressId++,
                PhoneNumber = input.PhoneNumber,
                Name = input.Name,
                Value = input.Value,
                City = input.City,
                ZipCode = input.ZipCode,
                Country = input.Country
            };

            _addresses.Add(address);
            return address;
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
