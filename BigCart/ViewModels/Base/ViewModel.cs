using BigCart.DependencyInjection;
using BigCart.Services.Modal;
using BigCart.Services.Navigation;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public abstract class ViewModel : ObservableObject, IDependency
    {
        protected readonly INavigationService _navigationService;
        protected readonly IModalService _modalService;

        public ICommand GoBackCommand { get; protected set; }

        public ViewModel()
        {
            _navigationService = DependencyResolver.Get<INavigationService>();
            _modalService = DependencyResolver.Get<IModalService>();
            GoBackCommand = new AsyncCommand(async () =>
            {
                if (!OnBackButtonPressed())
                    await _navigationService.PopAsync();
            });
        }

        public virtual void Initialize(object navigationData) { }
        public virtual void OnStart() { }
        public virtual void OnResume() { }
        public virtual bool OnBackButtonPressed() => false;
        public virtual void OnPause() { }
        public virtual void OnStop() { }
    }
}
