using Acr.UserDialogs;
using BigCart.DependencyInjection;
using System.Threading.Tasks;

namespace BigCart.Services.Modal
{
    public class ModalService : IModalService, ISingletonDependency
    {
        public Task AlertAsync(string message, string title = null, string okText = null)
        {
            return UserDialogs.Instance.AlertAsync(message, title, okText);
        }

        public void ShowLoading(string text)
        {
            UserDialogs.Instance.ShowLoading(text);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
