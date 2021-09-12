using BigCart.Pages;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private int _currentTabIndex;

        public int CurrentTabIndex
        {
            get => _currentTabIndex;
            set => SetProperty(ref _currentTabIndex, value);
        }
        public ICommand GoToTabCommand { get; }
        public ICommand ViewCartCommand { get; }

        public MainViewModel()
        {
            GoToTabCommand = new Command<int>(tabIndex => CurrentTabIndex = tabIndex);
            ViewCartCommand = new AsyncCommand(() => _navigationService.PushAsync<CartPage>(), allowsMultipleExecutions: false);
        }
    }
}
