using BigCart.Pages;
using BigCart.Services.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class WelcomeViewModel : ViewModel
    {
        public ICommand LoginCommand { get; }
        public ICommand SignupCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public WelcomeViewModel()
        {
            LoginCommand = SignupCommand = new AsyncCommand(GoToMainPageAsync, allowsMultipleExecutions: false);
            ForgotPasswordCommand = new AsyncCommand(() => _navigationService.PushAsync<ForgotPasswordPage>(), allowsMultipleExecutions: false);
        }

        private Task GoToMainPageAsync()
        {
            return _navigationService.PushAsync<MainPage>(new NavigationOptions
            {
                ClearHistory = false
            });
        }
    }
}
