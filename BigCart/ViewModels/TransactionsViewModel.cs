using BigCart.Models;
using BigCart.Services.Transactions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace BigCart.ViewModels
{
    public class TransactionsViewModel : ViewModel
    {
        private readonly ITransactionService _transactionService;
        private LayoutState _loadState;

        public LayoutState LoadState
        {
            get => _loadState;
            private set => SetProperty(ref _loadState, value);
        }
        public Transaction[] Transactions { get; private set; }

        public TransactionsViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = LoadTransactionsAsync();
        }

        private async Task LoadTransactionsAsync()
        {
            LoadState = LayoutState.Loading;

            Transactions = await _transactionService.GetTransactionsAsync();
            OnPropertyChanged(nameof(Transactions));

            LoadState = (Transactions.Length > 0) ? LayoutState.Success : LayoutState.Empty;
        }
    }
}
