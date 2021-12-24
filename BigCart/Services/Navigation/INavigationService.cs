using BigCart.Pages;
using System.Threading.Tasks;

namespace BigCart.Services.Navigation
{
    public interface INavigationService
    {
        void Initialize();
        Task PushAsync<T>(NavigationOptions options = null) where T : Page;
        Task PopAsync();
        Task PopToRootAsync();
    }
}
