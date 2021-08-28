using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class ViewModel : ObservableObject
    {
        public virtual void OnStart() { }
        public virtual void OnResume() { }
        public virtual bool OnBackButtonPressed() => false;
        public virtual void OnPause() { }
        public virtual void OnStop() { }
    }
}
