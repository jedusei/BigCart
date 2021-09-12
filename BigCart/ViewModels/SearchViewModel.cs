using BigCart.Modals;
using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Cart;
using BigCart.Services.Products;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class SearchViewModel : ViewModel
    {
        public const int SEARCH_DEBOUNCE_TIME = 500;
        private readonly IProductService _productService;
        private LayoutState _state = LayoutState.None;
        private string _query;
        private ProductFilter _filter = new();
        private Product[] _results;
        private int _currentSearchId;

        public LayoutState State
        {
            get => _state;
            private set => SetProperty(ref _state, value);
        }
        public string Query
        {
            get => _query;
            set => SetProperty(ref _query, value?.Trim(), onChanged: () => _ = SearchAsync());
        }
        public ObservableCollection<string> SearchHistory { get; }
        public Product[] Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }
        public ICommand SearchCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand SetQueryCommand { get; }
        public ICommand ClearHistoryCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ViewProductCommand { get; }
        public ICommand AddToCartCommand { get; }

        public SearchViewModel(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            SearchHistory = productService.GetSearchHistory();
            SearchCommand = new AsyncCommand(SearchAsync, allowsMultipleExecutions: false);
            ApplyFilterCommand = new AsyncCommand(ApplyFiltersAsync, allowsMultipleExecutions: false);
            ClearHistoryCommand = new AsyncCommand(ClearHistoryAsync, allowsMultipleExecutions: false);
            SetQueryCommand = new Command<string>(query => Query = query);
            ToggleFavoriteCommand = new Command<Product>(p => _productService.SetFavoriteStatus(p, !p.IsFavorite));
            ViewProductCommand = new AsyncCommand<Product>(p => _navigationService.PushAsync<ProductPage>(new() { Data = p }), allowsMultipleExecutions: false);
            AddToCartCommand = new Command<Product>(p => cartService.SetCartStatus(p, true));
        }

        private async Task SearchAsync()
        {
            if (string.IsNullOrEmpty(_query))
            {
                _currentSearchId++;
                State = LayoutState.None;
                return;
            }

            int searchId = ++_currentSearchId;
            await Task.Delay(SEARCH_DEBOUNCE_TIME);
            if (searchId != _currentSearchId)
                return;

            State = LayoutState.Loading;

            _filter.Name = _query;
            Results = await _productService.GetProductsAsync(_filter);
            for (int i = 0; i < _results.Length; i++)
                _results[i].Column = i % 2;

            State = (Results.Length > 0) ? LayoutState.Success : LayoutState.Empty;
        }

        private async Task ApplyFiltersAsync()
        {
            ProductFilter newFilter = await _modalService.PushAsync<FilterModal, ProductFilter>(_filter);
            if (_filter != newFilter)
            {
                _filter = newFilter;
                await SearchAsync();
            }
        }

        private async Task ClearHistoryAsync()
        {
            bool confirmed = await _modalService.ConfirmAsync("Are you sure you want to clear your search history?");
            if (confirmed)
                SearchHistory.Clear();
        }
    }
}
