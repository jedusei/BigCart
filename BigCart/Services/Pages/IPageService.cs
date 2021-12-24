using Xamarin.Forms;

namespace BigCart.Services.Pages
{
    public interface IPageService
    {
        Page MainPage { get; set; }
        T CreatePage<T>(object navigationData = null) where T : Page;
    }
}
