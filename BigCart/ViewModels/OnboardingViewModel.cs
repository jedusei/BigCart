using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class OnboardingViewModel : ViewModel
    {
        public ICommand GetStartedCommand { get; }

        public OnboardingViewModel()
        {
          //  GetStartedCommand = new AsyncCommand(()=>_navigationService.InitializeAsync)
        }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
        }
    }
}
