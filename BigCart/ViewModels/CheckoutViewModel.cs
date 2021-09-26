﻿using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Orders;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class CheckoutViewModel : ViewModel
    {
        private readonly IOrderService _orderService;
        private int _currentStep;
        private int _stepsCompleted;
        private PaymentMethod _paymentMethod = PaymentMethod.CreditCard;
        private CreditCardType _cardType = CreditCardType.Mastercard;
        private string _cardNumber;
        private string _cardHolder;
        private DateTime? _cardExpiryDate;

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
        public CreditCardType CardType
        {
            get => _cardType;
            private set => SetProperty(ref _cardType, value);
        }
        public string CardNumber
        {
            get => _cardNumber;
            set => SetProperty(ref _cardNumber, value?.Trim(), onChanged: UpdateCardType);
        }
        public string CardHolder
        {
            get => _cardHolder;
            set => SetProperty(ref _cardHolder, value?.Trim());
        }
        public DateTime? CardExpiryDate
        {
            get => _cardExpiryDate;
            set => SetProperty(ref _cardExpiryDate, value);
        }
        public ICommand SetCardTypeCommand { get; }
        public ICommand NextStepCommand { get; }

        public CheckoutViewModel(IOrderService orderService)
        {
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

        private void UpdateCardType()
        {
            if (_cardNumber?.Length > 0)
            {
                switch (_cardNumber[0])
                {
                    case '4':
                        CardType = CreditCardType.Visa;
                        break;

                    default:
                        CardType = CreditCardType.Mastercard;
                        break;
                }
            }
        }

        private async Task NextStepAsync()
        {
            if (_currentStep < 2)
            {
                CurrentStep++;
                if (_stepsCompleted < _currentStep)
                    StepsCompleted++;
            }
            else
            {
                StepsCompleted = 3;
                await Task.Delay(1000);
                _modalService.ShowLoading("Making payment...");

                await _orderService.CreateOrderAsync(new(_paymentMethod, _cardType));

                _modalService.HideLoading();
                await _navigationService.PushAsync<OrderSuccessPage>();
            }
        }
    }
}
