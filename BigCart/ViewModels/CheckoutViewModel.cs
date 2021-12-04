using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Address;
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
        private readonly IAddressService _addressService;
        private readonly ICreditCardService _creditCardService;
        private readonly IOrderService _orderService;
        private int _currentStep;
        private int _stepsCompleted;
        private DeliveryMethod _deliveryMethod;
        private PaymentMethod _paymentMethod;
        private Address _address;
        private bool _hasLoadedAddress;
        private CreditCard _card = new();
        private CreditCard _defaultCard;
        private bool _hasLoadedCard;
        private bool _saveAddress = true;
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
        public DeliveryMethod DeliveryMethod
        {
            get => _deliveryMethod;
            set => SetProperty(ref _deliveryMethod, value);
        }
        public Address Address
        {
            get => _address;
            private set => SetProperty(ref _address, value);
        }
        public bool SaveAddress
        {
            get => _saveAddress;
            set => SetProperty(ref _saveAddress, value);
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
        public ICommand SetDeliveryMethodCommand { get; }
        public ICommand SetPaymentMethodCommand { get; }
        public ICommand NextStepCommand { get; }

        public CheckoutViewModel(IAddressService addressService, ICreditCardService creditCardService, IOrderService orderService)
        {
            _addressService = addressService;
            _creditCardService = creditCardService;
            _orderService = orderService;

            UpdateTitle();

            SetDeliveryMethodCommand = new Command<DeliveryMethod>(value => DeliveryMethod = value);
            SetPaymentMethodCommand = new Command<PaymentMethod>(value => PaymentMethod = value);
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

                if (_currentStep == 1)
                {
                    if (!_hasLoadedAddress)
                    {
                        await Task.Delay(400);
                        await GetAddressAsync();
                        _hasLoadedAddress = true;
                    }
                }
                else if (_currentStep == 2)
                {
                    if (!_hasLoadedCard)
                    {
                        await Task.Delay(400);
                        await GetCreditCardAsync();
                        _hasLoadedCard = true;
                    }
                }
            }
            else
            {
                StepsCompleted = 3;
                await MakePaymentAsync();
            }
        }

        private async Task GetAddressAsync()
        {
            _modalService.ShowLoading("Getting address details...");

            Address = await _addressService.GetDefaultAddressAsync() ?? new();

            _modalService.HideLoading();
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

            Order order = await _orderService.CreateOrderAsync(new(_paymentMethod, _address, (_paymentMethod == PaymentMethod.CreditCard) ? _card : null, _deliveryMethod));

            if (_saveAddress)
            {
                if (_address.Id != 0)
                    await _addressService.UpdateAddressAsync(_address);
                else
                {
                    await _addressService.AddAddressAsync(new()
                    {
                        Name = _address.Name,
                        PhoneNumber = _address.PhoneNumber,
                        Value = _address.Value,
                        City = _address.City,
                        ZipCode = _address.ZipCode,
                        Country = _address.Country
                    });
                }
            }

            if (_saveCard)
            {
                if (_defaultCard == null || _card.Number != _defaultCard.Number)
                    await _creditCardService.AddCardAsync(_card);
                else
                    await _creditCardService.UpdateCardAsync(_card);
            }

            _modalService.HideLoading();
            await _navigationService.PushAsync<OrderSuccessPage>(new() { Data = order });
        }
    }
}
