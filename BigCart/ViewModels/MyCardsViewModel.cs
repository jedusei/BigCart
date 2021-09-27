using BigCart.Modals;
using BigCart.Models;
using BigCart.Services.CreditCards;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class MyCardsViewModel : ViewModel
    {
        private readonly ICreditCardService _creditCardService;
        private LayoutState _loadState;

        public LayoutState LoadState
        {
            get => _loadState;
            set => SetProperty(ref _loadState, value);
        }
        public ReadOnlyObservableCollection<CreditCard> CreditCards { get; private set; }
        public IAsyncCommand AddCardCommand { get; }
        public ICommand SetDefaultCardCommand { get; }

        public MyCardsViewModel(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
            AddCardCommand = new AsyncCommand(AddCardAsync);
            SetDefaultCardCommand = new Command<CreditCard>(card =>
            {
                if (!card.IsDefault)
                    _creditCardService.SetDefaultCard(card.Number);
            });
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = GetCardsAsync();
        }

        private async Task GetCardsAsync()
        {
            LoadState = LayoutState.Loading;

            CreditCards = await _creditCardService.GetCardsAsync();
            OnPropertyChanged(nameof(CreditCards));

            LoadState = (CreditCards.Count > 0) ? LayoutState.Success : LayoutState.Empty;
        }

        private async Task AddCardAsync()
        {
            CreditCard newCard = await _modalService.PushAsync<AddCardModal, CreditCard>();
            if (newCard != null)
            {
                _modalService.ShowLoading("Adding...");
                await _creditCardService.AddCardAsync(newCard);
                _modalService.HideLoading();
            }
        }
    }
}
