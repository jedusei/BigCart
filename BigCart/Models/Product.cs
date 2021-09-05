using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.Models
{
    public class Product : ObservableObject
    {
        private bool _isInCart;
        private int _quantity;
        private bool _isFavorite;

        public int Column { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public ImageSource ImageSource { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public float Weight { get; set; }
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        public bool IsInCart
        {
            get => _isInCart;
            set => SetProperty(ref _isInCart, value);
        }
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }
    }
}
