using BigCart.DependencyInjection;
using BigCart.Pages;
using BigCart.Services.Pages;
using BigCart.Services.Platform;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Page = BigCart.Pages.Page;

namespace BigCart.Services.Navigation
{
    public class NavigationService : INavigationService, ISingletonDependency
    {
        private readonly IPlatformService _platformService;
        private readonly IPageService _pageService;

        public NavigationService(IPlatformService platformService, IPageService pageService)
        {
            _platformService = platformService;
            _pageService = pageService;
        }

        public void Initialize()
        {
            _pageService.MainPage = new NavigationPage(_pageService.CreatePage<OnboardingPage>());
        }

        public async Task PushAsync<T>(NavigationOptions options = null) where T : Page
        {
            T page = _pageService.CreatePage<T>(options?.Data);
            INavigation navigation = _pageService.MainPage.Navigation;

            // Disable input on current page
            var currentPage = (ContentPage)navigation.NavigationStack[^1];
            currentPage.InputTransparent = true;

            await navigation.PushAsync(page);

            currentPage.InputTransparent = false;

            if (options != null)
            {
                if (options.ClearHistory)
                {
                    int startIndex = navigation.NavigationStack.Count - 2;
                    for (int i = startIndex; i >= 0; i--)
                    {
                        Xamarin.Forms.Page p = navigation.NavigationStack[i];
                        navigation.RemovePage(p);

                        if (i < startIndex && p is Page)
                            (p as Page).Stop();
                    }
                }
                else if (options.Replace)
                {
                    navigation.RemovePage(navigation.NavigationStack[^2]);
                }
            }
        }

        public async Task PopAsync()
        {
            INavigation navigation = _pageService.MainPage.Navigation;
            if (navigation.NavigationStack.Count > 1)
                await navigation.PopAsync();
            else
                _platformService.Quit();
        }

        public async Task PopToRootAsync()
        {
            INavigation navigation = _pageService.MainPage.Navigation;
            if (navigation.NavigationStack.Count > 1)
            {
                var pagesInBetween = navigation.NavigationStack.Skip(1)
                    .Take(navigation.NavigationStack.Count - 2)
                    .ToArray();

                await navigation.PopToRootAsync();

                foreach (Xamarin.Forms.Page page in pagesInBetween)
                {
                    if (page is Page p)
                        p.Stop();
                }
            }
        }
    }
}
