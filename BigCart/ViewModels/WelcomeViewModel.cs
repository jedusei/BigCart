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
            LoginCommand = new AsyncCommand(() => _navigationService.PushAsync<MainPage>(new() { ClearHistory = true }), allowsMultipleExecutions: false);
            SignupCommand = new AsyncCommand(() => _navigationService.PushAsync<VerifyNumberPage>(), allowsMultipleExecutions: false);
            ForgotPasswordCommand = new AsyncCommand(() => _navigationService.PushAsync<ForgotPasswordPage>(), allowsMultipleExecutions: false);
        }
    }
}
