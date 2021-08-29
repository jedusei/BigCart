using BigCart.DependencyInjection;
using BigCart.Pages;
using BigCart.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Page = BigCart.Views.Page;

namespace BigCart.Services.Navigation
{
    class NavigationService : INavigationService, ISingletonDependency
    {
        public async Task InitializeAsync()
        {
            App.Current.MainPage = new NavigationPage(CreatePage<OnboardingPage>());
            await Task.CompletedTask;
        }

        private T CreatePage<T>(object navigationData = null) where T : Page
        {
            T page = Activator.CreateInstance<T>();
            if (page.BindingContext is ViewModel viewModel)
                viewModel.Initialize(navigationData);

            return page;
        }
    }
}
