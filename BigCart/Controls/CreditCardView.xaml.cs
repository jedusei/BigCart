using BigCart.Models;
using System;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public partial class CreditCardView : ContentView
    {
        public static readonly BindableProperty CardTypeProperty = BindableProperty.Create(nameof(CardType), typeof(CreditCardType), typeof(CreditCardView), CreditCardType.Mastercard, BindingMode.OneWay);
        public static readonly BindableProperty CardNumberProperty = BindableProperty.Create(nameof(CardNumber), typeof(string), typeof(CreditCardView), CARD_NUMBER_PREFIX + "XXXX", propertyChanged: OnCardNumberChanged);
        public static readonly BindableProperty CardHolderProperty = BindableProperty.Create(nameof(CardHolder), typeof(string), typeof(CreditCardView), propertyChanged: OnCardHolderChanged);
        public static readonly BindableProperty ExpiryDateProperty = BindableProperty.Create(nameof(ExpiryDate), typeof(DateTime?), typeof(CreditCardView), propertyChanged: OnExpiryDateChanged);
        private const string CARD_NUMBER_PREFIX = "XXXX XXXX XXXX ";
        private Label _txtCardNumber, _txtCardHolder, _txtExpiryDate;

        public CreditCardType CardType
        {
            get => (CreditCardType)GetValue(CardTypeProperty);
            set => SetValue(CardTypeProperty, value);
        }
        public string CardNumber
        {
            get => (string)GetValue(CardNumberProperty);
            set => SetValue(CardNumberProperty, value);
        }
        public string CardHolder
        {
            get => (string)GetValue(CardHolderProperty);
            set => SetValue(CardHolderProperty, value);
        }
        public DateTime? ExpiryDate
        {
            get => (DateTime?)GetValue(ExpiryDateProperty);
            set => SetValue(ExpiryDateProperty, value);
        }

        public CreditCardView()
        {
            InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _txtCardNumber = (Label)GetTemplateChild("txtCardNumber");
            _txtCardHolder = (Label)GetTemplateChild("txtCardHolder");
            _txtExpiryDate = (Label)GetTemplateChild("txtExpiryDate");

            UpdateCardHolder();
            UpdateCardNumber();
            UpdateExpiryDate();
        }

        private static void OnCardNumberChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (CreditCardView)bindable;
            view.UpdateCardNumber();
        }
        private static void OnCardHolderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (CreditCardView)bindable;
            view.UpdateCardHolder();
        }

        private static void OnExpiryDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (CreditCardView)bindable;
            view.UpdateExpiryDate();
        }

        private void UpdateCardNumber()
        {
            var cardNumber = CardNumber;
            if (string.IsNullOrWhiteSpace(cardNumber))
                _txtCardNumber.Text = (string)CardNumberProperty.DefaultValue;
            else
            {
                string lastFourDigits = (cardNumber.Length >= 4) ? cardNumber.Substring(cardNumber.Length - 4) : cardNumber;

                if (lastFourDigits.Length < 4)
                    lastFourDigits = new string('X', 4 - lastFourDigits.Length) + lastFourDigits;

                _txtCardNumber.Text = CARD_NUMBER_PREFIX + lastFourDigits;
            }
        }

        private void UpdateCardHolder()
        {
            _txtCardHolder.Text = CardHolder;
        }

        private void UpdateExpiryDate()
        {
            _txtExpiryDate.Text = ExpiryDate.HasValue ? ExpiryDate.Value.ToString("MM/yy") : " ";
        }
    }
}