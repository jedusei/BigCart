using BigCart.Pages;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class WelcomeViewModel : ViewModel
    {
        public ICommand ForgotPasswordCommand { get; }

        public WelcomeViewModel()
        {
            ForgotPasswordCommand = new AsyncCommand(() => _navigationService.PushAsync<ForgotPasswordPage>(), allowsMultipleExecutions: false);
        }
    }
}
