using BigCart.Services.Navigation;
using BigCart.Pages;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class OnboardingViewModel : ViewModel
    {
        static int count;
        public ICommand GetStartedCommand { get; }

        public OnboardingViewModel()
        {
            GetStartedCommand = new AsyncCommand(() => _navigationService.PushAsync<OnboardingPage>(new NavigationOptions { ClearHistory = (count > 3) }));
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            if (count > 3)
                count = 0;

            count++;
        }
    }
}
