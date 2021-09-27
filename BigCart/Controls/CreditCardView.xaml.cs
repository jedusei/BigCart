using BigCart.Models;
using System;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public partial class CreditCardView : ContentView
    {
        public static readonly BindableProperty CardTypeProperty = BindableProperty.Create(nameof(CardType), typeof(CreditCardType), typeof(CreditCardView), CreditCardType.Mastercard, BindingMode.OneWay);
        public static readonly BindableProperty CardNumberProperty = BindableProperty.Create(nameof(CardNumber), typeof(string), typeof(CreditCardView));
        public static readonly BindableProperty CardHolderProperty = BindableProperty.Create(nameof(CardHolder), typeof(string), typeof(CreditCardView), propertyChanged: OnCardHolderChanged);
        public static readonly BindableProperty ExpiryDateProperty = BindableProperty.Create(nameof(ExpiryDate), typeof(DateTime?), typeof(CreditCardView), propertyChanged: OnExpiryDateChanged);
        private Label _txtCardHolder, _txtExpiryDate;

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

            _txtCardHolder = (Label)GetTemplateChild("txtCardHolder");
            _txtExpiryDate = (Label)GetTemplateChild("txtExpiryDate");

            UpdateCardHolder();
            UpdateExpiryDate();
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