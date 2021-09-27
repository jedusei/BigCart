using BigCart.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class AddCardViewModel : ModalViewModel
    {
        private DateTime? _expiryDate;

        public CreditCard Card { get; } = new();
        public DateTime? ExpiryDate
        {
            get => _expiryDate;
            set => SetProperty(ref _expiryDate, value);
        }
        public ICommand AddCommand { get; }

        public AddCardViewModel()
        {
            AddCommand = new Command(() =>
            {
                Card.ExpiryDate = _expiryDate ?? DateTime.Today.AddYears(1);
                SetResult(Card);
            });
        }
    }
}
