using BigCart.Messaging;
using BigCart.Pages;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class AccountViewModel : ViewModel
    {
        public ICommand OpenMenuItemCommand { get; }

        public AccountViewModel()
        {
            OpenMenuItemCommand = new AsyncCommand<string>(OpenMenuItemAsync, allowsMultipleExecutions: false);
        }

        private async Task OpenMenuItemAsync(string menuItemId)
        {
            switch (menuItemId)
            {
                case AccountTab.MENU_ITEM_ABOUT:
                    await _navigationService.PushAsync<AboutPage>();
                    break;

                case AccountTab.MENU_ITEM_ORDERS:
                    await _navigationService.PushAsync<MyOrdersPage>();
                    break;

                case AccountTab.MENU_ITEM_FAVORITES:
                    MessagingCenter.Send((object)this, MessageKeys.GoToTab, MainPage.TAB_FAVORITES);
                    break;

                case AccountTab.MENU_ITEM_NOTIFICATIONS:
                    await _navigationService.PushAsync<NotificationsPage>();
                    break;

                case AccountTab.MENU_ITEM_LOGOUT:
                    await LogoutAsync();
                    break;
            }
        }

        private async Task LogoutAsync()
        {
            bool confirmed = await _modalService.ConfirmAsync("Are you sure you want to sign out?", "Sign Out");
            if (confirmed)
                await _navigationService.PushAsync<WelcomePage>(new() { ClearHistory = true });
        }
    }
}
