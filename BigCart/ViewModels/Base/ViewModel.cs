using BigCart.DependencyInjection;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public abstract class ViewModel : ObservableObject, IDependency
    {
        public virtual void Initialize(object navigationData) { }
        public virtual void OnStart() { }
        public virtual void OnResume() { }
        public virtual bool OnBackButtonPressed() => false;
        public virtual void OnPause() { }
        public virtual void OnStop() { }
    }
}
