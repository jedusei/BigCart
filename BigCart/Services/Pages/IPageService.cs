using BigCart.DependencyInjection;
using Xamarin.Forms;

namespace BigCart.Services.Pages
{
    public interface IPageService : IDependency
    {
        Page MainPage { get; set; }
        T CreatePage<T>(object navigationData = null) where T : Page;
    }
}
