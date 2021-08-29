using BigCart.DependencyInjection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart.Services.Navigation
{
    class NavigationService : INavigationService, ISingletonDependency
    {
        public async Task InitializeAsync()
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
            await Task.CompletedTask;
        }
    }
}
