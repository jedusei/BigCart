using BigCart.Pages;
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
            LoginCommand = SignupCommand = new AsyncCommand(() => _navigationService.PushAsync<MainPage>(new() { ClearHistory = true }), allowsMultipleExecutions: false);
            ForgotPasswordCommand = new AsyncCommand(() => _navigationService.PushAsync<ForgotPasswordPage>(), allowsMultipleExecutions: false);
        }
    }
}
