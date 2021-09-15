using BigCart.DependencyInjection;
using BigCart.Pages;
using System.Threading.Tasks;

namespace BigCart.Services.Navigation
{
    public interface INavigationService : IDependency
    {
        void Initialize();
        Task PushAsync<T>(NavigationOptions options = null) where T : Page;
        Task PopAsync();
        Task PopToRootAsync();
    }
}
