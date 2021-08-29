using BigCart.DependencyInjection;
using BigCart.ViewModels;
using BigCart.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Page = BigCart.Pages.Page;

namespace BigCart.Services.Navigation
{
    class NavigationService : INavigationService, ISingletonDependency
    {
        public void Initialize()
        {
            App.Current.MainPage = new NavigationPage(CreatePage<OnboardingPage>());
        }

        public async Task PushAsync<T>(NavigationOptions options = null) where T : Page
        {
            T page = CreatePage<T>(options?.Data);
            INavigation navigation = GetNavigation();
            await navigation.PushAsync(page);

            if (options != null)
            {
                if (options.ClearHistory)
                {
                    for (int i = navigation.NavigationStack.Count - 2; i >= 0; i--)
                        navigation.RemovePage(navigation.NavigationStack[i]);
                }
                else if (options.Replace)
                {
                    navigation.RemovePage(navigation.NavigationStack[^2]);
                }
            }
        }

        public Task PopAsync()
        {
            return GetNavigation().PopAsync();
        }

        private T CreatePage<T>(object navigationData = null) where T : Page
        {
            T page = Activator.CreateInstance<T>();
            if (page.BindingContext is ViewModel viewModel)
                viewModel.Initialize(navigationData);

            return page;
        }

        INavigation GetNavigation()
        {
            return Application.Current.MainPage.Navigation;
        }
    }
}
