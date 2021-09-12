using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Cart;
using BigCart.Services.Products;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        private readonly IProductService _productService;
        private LoadStatus _loadStatus;

        public LoadStatus LoadStatus
        {
            get => _loadStatus;
            set => SetProperty(ref _loadStatus, value);
        }
        public Product[] Products { get; private set; }
        public ICommand OpenSearchCommand { get; }
        public ICommand ViewCategoriesCommand { get; }
        public ICommand ViewCategoryCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ViewProductCommand { get; }
        public ICommand AddToCartCommand { get; }

        public HomeViewModel(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            OpenSearchCommand = new AsyncCommand(() => _navigationService.PushAsync<SearchPage>());
            ViewCategoriesCommand = new AsyncCommand(() => _navigationService.PushAsync<CategoriesPage>());
            ViewCategoryCommand = new AsyncCommand<Category>(category => _navigationService.PushAsync<CategoryPage>(new() { Data = category }));
            ViewProductCommand = new AsyncCommand<Product>(product => _navigationService.PushAsync<ProductPage>(new() { Data = product }));
            ToggleFavoriteCommand = new Command<Product>(p => _productService.SetFavoriteStatus(p, !p.IsFavorite));
            AddToCartCommand = new Command<Product>(p => cartService.SetCartStatus(p, true));
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            LoadStatus = LoadStatus.Loading;

            Products = await _productService.GetProductsAsync();
            for (int i = 0; i < Products.Length; i++)
                Products[i].Column = i % 2;

            OnPropertyChanged(nameof(Products));

            LoadStatus = LoadStatus.Success;
        }
    }
}
