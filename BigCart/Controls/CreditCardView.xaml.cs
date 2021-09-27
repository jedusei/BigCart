using BigCart.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public partial class CreditCardView : ContentView
    {
        public static readonly BindableProperty CardProperty = BindableProperty.Create(nameof(Card), typeof(CreditCard), typeof(CreditCardView), propertyChanged: OnCardChanged);
        private Label _txtExpiryDate;

        public CreditCard Card
        {
            get => (CreditCard)GetValue(CardProperty);
            set => SetValue(CardProperty, value);
        }

        public CreditCardView()
        {
            InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _txtExpiryDate = (Label)GetTemplateChild("txtExpiryDate");
            UpdateExpiryDate();
        }

        private static void OnCardChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (CreditCardView)bindable;
            view.OnCardChanged(oldValue as CreditCard, newValue as CreditCard);
        }

        private void OnCardChanged(CreditCard oldCard, CreditCard newCard)
        {
            if (oldCard != null)
                oldCard.PropertyChanged -= OnCardPropertyChanged;

            if (newCard != null)
                newCard.PropertyChanged += OnCardPropertyChanged;

            UpdateExpiryDate();
        }

        private void OnCardPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreditCard.ExpiryDate))
                UpdateExpiryDate();
        }

        private void UpdateExpiryDate()
        {
            _txtExpiryDate.Text = Card?.ExpiryDate?.ToString("MM/yy") ?? " ";
        }
    }
}