using BigCart.DependencyInjection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart.Services.Modal
{
    public interface IModalService : IDependency
    {
        Task AlertAsync(string message, string title = null, string okText = null);
        void ShowLoading(string text);
        void HideLoading();
        Task PushAsync<TModal>(object args = null) where TModal : Page;
        Task<TResult> PushAsync<TModal, TResult>(object args = null) where TModal : BigCart.Pages.ModalPage;
        Task PopAsync();
    }
}
