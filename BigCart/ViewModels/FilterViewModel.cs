using BigCart.Modals;
using BigCart.Services.Products;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class FilterViewModel : ModalViewModel
    {
        ProductFilter _originalFilter, _filter;

        public ProductFilter Filter
        {
            get => _filter;
            private set => SetProperty(ref _filter, value);
        }
        public ICommand ResetCommand { get; }
        public ICommand ToggleFilterCommand { get; }
        public ICommand ApplyCommand { get; }

        public FilterViewModel()
        {
            ResetCommand = new Command(() => Filter = _originalFilter?.Clone() ?? new ProductFilter());
            ApplyCommand = new Command(() => SetResult(Filter));
            ToggleFilterCommand = new Command<string>(option =>
            {
                switch (option)
                {
                    case FilterModal.OPTION_DISCOUNT:
                        _filter.Discount = !_filter.Discount;
                        break;
                    case FilterModal.OPTION_FREE_SHIPPING:
                        _filter.FreeShipping = !_filter.FreeShipping;
                        break;
                    case FilterModal.OPTION_SAME_DAY_DELIVERY:
                        _filter.SameDayDelivery = !_filter.SameDayDelivery;
                        break;
                }
            });
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            _originalFilter = navigationData as ProductFilter;
            Filter = _originalFilter?.Clone() ?? new ProductFilter();
        }

        protected override object GetDefaultResult()
        {
            return _originalFilter;
        }
    }
}
