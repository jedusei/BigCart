using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.Services.Products
{
    public class ProductFilter : ObservableObject
    {
        private string _name;
        private float? _minPrice;
        private float? _maxPrice;
        private float _rating;
        private bool _discount;
        private bool _sameDayDelivery;
        private bool _freeShipping;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value?.Trim());
        }
        public string Category { get; set; }
        public float? MinPrice
        {
            get => _minPrice;
            set => SetProperty(ref _minPrice, value);
        }
        public float? MaxPrice
        {
            get => _maxPrice;
            set => SetProperty(ref _maxPrice, value);
        }
        public float Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }
        public bool Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }
        public bool FreeShipping
        {
            get => _freeShipping;
            set => SetProperty(ref _freeShipping, value);
        }
        public bool SameDayDelivery
        {
            get => _sameDayDelivery;
            set => SetProperty(ref _sameDayDelivery, value);
        }

        public ProductFilter Clone()
        {
            return (ProductFilter)MemberwiseClone();
        }
    }
}
