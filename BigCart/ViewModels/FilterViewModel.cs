using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class FilterViewModel : ModalViewModel
    {
        public ICommand ApplyCommand { get; }

        public FilterViewModel()
        {
            ApplyCommand = new Command(() => SetResult(true));
        }

        protected override object GetDefaultResult()
        {
            return false;
        }
    }
}
