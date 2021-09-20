using BigCart.Models;
using BigCart.Services.Reviews;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class ReviewsViewModel : ViewModel
    {
        private readonly IReviewService _reviewService;
        private Product _product;
        private LayoutState _loadState;

        public LayoutState LoadState
        {
            get => _loadState;
            private set => SetProperty(ref _loadState, value);
        }
        public ReadOnlyObservableCollection<Review> Reviews { get; private set; }
        public ICommand AddReviewCommand { get; }

        public ReviewsViewModel(IReviewService reviewService)
        {
            _reviewService = reviewService;
            AddReviewCommand = new AsyncCommand(AddReviewAsync);
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            _product = (Product)navigationData;
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = LoadReviewsAsync();
        }

        private async Task LoadReviewsAsync()
        {
            LoadState = LayoutState.Loading;

            Reviews = await _reviewService.GetReviewsAsync(_product);
            OnPropertyChanged(nameof(Reviews));

            LoadState = (Reviews.Count > 0) ? LayoutState.Success : LayoutState.Empty;
        }

        private async Task AddReviewAsync()
        {
            await _reviewService.AddReviewAsync(_product, new Review
            {
                User = new User { Name = "You", ProfilePicture = ImageSource.FromFile("avatar.png") },
                Rating = 3,
                Comment = "This is a test"
            });
        }
    }
}
