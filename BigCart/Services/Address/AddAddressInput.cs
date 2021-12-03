namespace BigCart.Services.Address
{
    public record AddAddressInput
    {
        public string Name { get; init; }
        public string PhoneNumber { get; init; }
        public string Value { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public string Country { get; init; }
    }
}
