using BigCart.Modals;
using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Products;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class CategoryViewModel : ViewModel
    {
        private readonly IProductService _productService;
        private LoadStatus _loadStatus;
        private ProductFilter _filter;

        public LoadStatus LoadStatus
        {
            get => _loadStatus;
            private set => SetProperty(ref _loadStatus, value);
        }
        public Category Category { get; private set; }
        public Product[] Products { get; private set; }
        public ICommand LoadCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ViewProductCommand { get; }
        public ICommand AddToCartCommand { get; }

        public CategoryViewModel(IProductService productService)
        {
            _productService = productService;
            LoadCommand = new AsyncCommand(LoadProductsAsync);
            FilterCommand = new AsyncCommand(FilterProductsAsync, allowsMultipleExecutions: false);
            ToggleFavoriteCommand = new Command<Product>(p => p.IsFavorite = !p.IsFavorite);
            ViewProductCommand = new AsyncCommand<Product>(p => _navigationService.PushAsync<ProductPage>(new() { Data = p }), allowsMultipleExecutions: false);
            AddToCartCommand = new Command<Product>(p =>
            {
                p.IsInCart = true;
                p.Quantity = 1;
            });
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            Category = (Category)navigationData;
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(FilterCommand));
            _filter = new ProductFilter { Category = Category.Name };
        }

        public override void OnStart()
        {
            base.OnStart();
            LoadStatus = LoadStatus.Loading;
        }

        private async Task LoadProductsAsync()
        {
            LoadStatus = LoadStatus.Loading;

            Products = await _productService.GetProductsAsync(_filter);

            for (int i = 0; i < Products.Length; i++)
                Products[i].Column = i % 2;

            OnPropertyChanged(nameof(Products));

            LoadStatus = LoadStatus.Success;
        }

        private async Task FilterProductsAsync()
        {
            ProductFilter newFilter = await _modalService.PushAsync<FilterModal, ProductFilter>(_filter);
            if (_filter != newFilter)
            {
                _filter = newFilter;
                await LoadProductsAsync();
            }
        }
    }
}
