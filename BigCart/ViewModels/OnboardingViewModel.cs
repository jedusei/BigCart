using BigCart.Pages;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class OnboardingViewModel : ViewModel
    {
        private int _currentSlideIndex;

        public object[] Slides { get; } = new[] {
            new
            {
                ShowLogo = true,
                Title = "Welcome to",
                Image = ImageSource.FromFile("onboarding_1.png")
            },
            new
            {
                ShowLogo = false,
                Title = "Buy Quality Dairy Products",
                Image = ImageSource.FromFile("onboarding_2.png")
            },
            new
            {
                ShowLogo = false,
                Title = "Buy Premium Quality Fruits",
                Image = ImageSource.FromFile("onboarding_3.png")
            },
            new
            {
                ShowLogo = false,
                Title = "Get Discounts On All Products",
                Image = ImageSource.FromFile("onboarding_4.png")
            }
        };
        public int CurrentSlideIndex
        {
            get => _currentSlideIndex;
            set => SetProperty(ref _currentSlideIndex, value);
        }
        public ICommand NextCommand { get; }

        public OnboardingViewModel()
        {
            NextCommand = new AsyncCommand(NextSlideAsync, allowsMultipleExecutions: false);
        }

        private async Task NextSlideAsync()
        {
            if (_currentSlideIndex < Slides.Length - 1)
                CurrentSlideIndex++;
            else
                await _navigationService.PushAsync<WelcomePage>();
        }
    }
}
