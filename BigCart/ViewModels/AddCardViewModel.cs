using BigCart.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class AddCardViewModel : ModalViewModel
    {
        public CreditCard Card { get; } = new();
        public ICommand AddCommand { get; }

        public AddCardViewModel()
        {
            AddCommand = new Command(() => SetResult(Card));
        }
    }
}
