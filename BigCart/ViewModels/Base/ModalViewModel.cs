using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public abstract class ModalViewModel : ViewModel
    {
        private readonly TaskCompletionSource<object> _resultTcs = new();

        public ModalViewModel()
        {
            GoBackCommand = new AsyncCommand(async () =>
            {
                if (!OnBackButtonPressed())
                    await _modalService.PopAsync();
            });
        }

        public Task<object> GetResultAsync()
        {
            return _resultTcs.Task;
        }

        public void SetResult(object result)
        {
            _resultTcs.SetResult(result);
        }

        protected virtual object GetDefaultResult()
        {
            return null;
        }

        public override void OnStop()
        {
            base.OnStop();
            _resultTcs.TrySetResult(GetDefaultResult());
        }
    }
}
