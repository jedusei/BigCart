using BigCart.DependencyInjection;
using System.Threading.Tasks;

namespace BigCart.Services.Modal
{
    public interface IModalService : IDependency
    {
        Task AlertAsync(string message, string title = null, string okText = null);
        void ShowLoading(string text);
        void HideLoading();
    }
}
