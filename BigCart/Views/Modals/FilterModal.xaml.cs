using BigCart.Pages;

namespace BigCart.Modals
{
    public partial class FilterModal : ModalPage
    {
        public const string OPTION_DISCOUNT = "Discount";
        public const string OPTION_FREE_SHIPPING = "Free Shipping";
        public const string OPTION_SAME_DAY_DELIVERY = "Same day delivery";

        public FilterModal()
        {
            InitializeComponent();
        }
    }
}