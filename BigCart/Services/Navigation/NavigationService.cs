using BigCart.DependencyInjection;
using BigCart.Pages;
using BigCart.Services.Platform;
using BigCart.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Page = BigCart.Pages.Page;

namespace BigCart.Services.Navigation
{
    public class NavigationService : INavigationService, ISingletonDependency
    {
        private readonly IPlatformService _platformService;

        public NavigationService(IPlatformService platformService)
        {
            _platformService = platformService;
        }

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

        public async Task PopAsync()
        {
            INavigation navigation = GetNavigation();
            if (navigation.NavigationStack.Count > 1)
                await navigation.PopAsync();
            else
                _platformService.Quit();
        }

        private T CreatePage<T>(object navigationData = null) where T : Page
        {
            T page = Activator.CreateInstance<T>();
            if (page.BindingContext is ViewModel viewModel)
                viewModel.Initialize(navigationData);

            return page;
        }

        private INavigation GetNavigation()
        {
            return Application.Current.MainPage.Navigation;
        }
    }
}
