using BigCart.DependencyInjection;
using BigCart.Services.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public abstract class ViewModel : ObservableObject, IDependency
    {
        protected readonly INavigationService _navigationService;

        public ViewModel()
        {
            _navigationService = DependencyResolver.Get<INavigationService>();
        }

        public virtual void Initialize(object navigationData) { }
        public virtual void OnStart() { }
        public virtual void OnResume() { }
        public virtual bool OnBackButtonPressed() => false;
        public virtual void OnPause() { }
        public virtual void OnStop() { }
    }
}
