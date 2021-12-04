using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.Models
{
    public class Address : ObservableObject
    {
        private string _name;
        private string _phoneNumber;
        private string _value;
        private string _city;
        private string _zipCode;
        private Country _country;
        private bool _isDefault;
        private bool _isExpanded;

        public int Id { get; init; }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value?.Trim());
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value?.Trim());
        }
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value?.Trim());
        }
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value?.Trim());
        }
        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value?.Trim());
        }
        public Country Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }
        public bool IsDefault
        {
            get => _isDefault;
            set => SetProperty(ref _isDefault, value);
        }
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public Address Clone()
        {
            return (Address)MemberwiseClone();
        }

        public void CopyFrom(Address other)
        {
            _name = other._name;
            _phoneNumber = other._phoneNumber;
            _value = other._value;
            _city = other._city;
            _zipCode = other._zipCode;
            _country = other._country;
        }
    }
}
