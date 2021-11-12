using BigCart.Models;
using BigCart.Pages;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class OrderSuccessViewModel : ViewModel
    {
        private Order _order;

        public ICommand TrackOrderCommand { get; }

        public OrderSuccessViewModel()
        {
            TrackOrderCommand = new AsyncCommand(() => _navigationService.PushAsync<TrackOrderPage>(new() { Data = _order }));
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            _order = (Order)navigationData;
        }

        public override bool OnBackButtonPressed()
        {
            _ = _navigationService.PopToRootAsync();
            return true;
        }
    }
}
