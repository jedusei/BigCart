using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.CreditCards;
using BigCart.Services.Orders;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class CheckoutViewModel : ViewModel
    {
        private readonly ICreditCardService _creditCardService;
        private readonly IOrderService _orderService;
        private int _currentStep;
        private int _stepsCompleted;
        private PaymentMethod _paymentMethod = PaymentMethod.CreditCard;
        private CreditCard _card = new();
        private CreditCard _defaultCard;
        private bool _hasLoadedCard;
        private bool _saveCard = true;

        public string Title { get; private set; }
        public int CurrentStep
        {
            get => _currentStep;
            set => SetProperty(ref _currentStep, value, onChanged: UpdateTitle);
        }
        public int StepsCompleted
        {
            get => _stepsCompleted;
            private set => SetProperty(ref _stepsCompleted, value);
        }
        public PaymentMethod PaymentMethod
        {
            get => _paymentMethod;
            set => SetProperty(ref _paymentMethod, value);
        }
        public CreditCard Card
        {
            get => _card;
            private set => SetProperty(ref _card, value);
        }
        public bool SaveCard
        {
            get => _saveCard;
            set => SetProperty(ref _saveCard, value);
        }
        public ICommand SetCardTypeCommand { get; }
        public ICommand NextStepCommand { get; }

        public CheckoutViewModel(ICreditCardService creditCardService, IOrderService orderService)
        {
            _creditCardService = creditCardService;
            _orderService = orderService;

            UpdateTitle();

            SetCardTypeCommand = new Command<PaymentMethod>(cardType => PaymentMethod = cardType);
            NextStepCommand = new AsyncCommand(NextStepAsync, allowsMultipleExecutions: false);
        }

        public override bool OnBackButtonPressed()
        {
            if (_currentStep == 0)
                return base.OnBackButtonPressed();

            CurrentStep--;
            return true;
        }

        private void UpdateTitle()
        {
            Title = _currentStep switch
            {
                0 => "Shipping Method",
                1 => "Shipping Address",
                _ => "Payment Method"
            };

            OnPropertyChanged(nameof(Title));
        }

        private async Task NextStepAsync()
        {
            if (_currentStep < 2)
            {
                CurrentStep++;
                if (_stepsCompleted < _currentStep)
                    StepsCompleted++;

                if (_currentStep == 2 && !_hasLoadedCard)
                {
                    await Task.Delay(400);
                    await GetCreditCardAsync();
                    _hasLoadedCard = true;
                }
            }
            else
            {
                StepsCompleted = 3;
                await MakePaymentAsync();
            }
        }

        private async Task GetCreditCardAsync()
        {
            _modalService.ShowLoading("Fetching credit card details...");

            _defaultCard = await _creditCardService.GetDefaultCardAsync();
            if (_defaultCard != null)
            {
                Card = _defaultCard.Clone();
                _card.IsDefault = _card.IsExpanded = false;
            }

            _modalService.HideLoading();
        }

        private async Task MakePaymentAsync()
        {
            _modalService.ShowLoading("Making payment...");

            await _orderService.CreateOrderAsync(new(_paymentMethod, (_paymentMethod == PaymentMethod.CreditCard) ? _card : null));

            if (_saveCard)
            {
                if (_defaultCard == null || _card.Number != _defaultCard.Number)
                    await _creditCardService.AddCardAsync(_card);
                else
                    await _creditCardService.UpdateCardAsync(_card);
            }

            _modalService.HideLoading();
            await _navigationService.PushAsync<OrderSuccessPage>();
        }
    }
}
