using Acr.UserDialogs;
using BigCart.Services.Pages;
using BigCart.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart.Services.Modal
{
    public class ModalService : IModalService
    {
        private readonly IPageService _pageService;

        public ModalService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public Task AlertAsync(string message, string title = null, string okText = null)
        {
            return UserDialogs.Instance.AlertAsync(message, title, okText);
        }

        public Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText);
        }

        public void ShowLoading(string text)
        {
            UserDialogs.Instance.ShowLoading(text);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        public async Task PushAsync<T>(object args = null) where T : Page
        {
            T modal = _pageService.CreatePage<T>(args);
            await _pageService.MainPage.Navigation.PushModalAsync(modal);
        }

        public async Task<TResult> PushAsync<TModalPage, TResult>(object args = null) where TModalPage : BigCart.Pages.ModalPage
        {
            TModalPage modal = _pageService.CreatePage<TModalPage>(args);
            INavigation navigation = _pageService.MainPage.Navigation;
            await navigation.PushModalAsync(modal);

            object result = default(TResult);
            if (modal.BindingContext is ModalViewModel viewModel)
            {
                result = await viewModel.GetResultAsync();
                if (navigation.ModalStack.Count > 0 && navigation.ModalStack[^1] == modal)
                    await PopAsync();
            }

            return (TResult)result;
        }

        public Task PopAsync()
        {
            return _pageService.MainPage.Navigation.PopModalAsync();
        }

    }
}
