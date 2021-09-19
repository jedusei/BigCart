using BigCart.Pages;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class OrderSuccessViewModel : ViewModel
    {
        public ICommand TrackOrderCommand { get; }

        public OrderSuccessViewModel()
        {
            TrackOrderCommand = new AsyncCommand(() => _navigationService.PushAsync<TrackOrderPage>());
        }

        public override bool OnBackButtonPressed()
        {
            _ = _navigationService.PopToRootAsync();
            return true;
        }
    }
}
