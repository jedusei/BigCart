using BigCart.Models;

namespace BigCart.Services.Address
{
    public record AddAddressInput
    {
        public string Name { get; init; }
        public string PhoneNumber { get; init; }
        public string Value { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public Country Country { get; init; }
    }
}
