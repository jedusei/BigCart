using BigCart.ViewModels;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public abstract class Tab : ContentView
    {
        private ViewModel _viewModel;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = BindingContext as ViewModel;
        }

        public virtual void OnStart()
        {
            _viewModel?.OnStart();
        }

        public virtual void OnPause()
        {
            _viewModel?.OnPause();
        }

        public virtual void OnResume()
        {
            _viewModel?.OnResume();
        }

        public virtual void OnStop()
        {
            _viewModel?.OnStop();
        }
    }
}
