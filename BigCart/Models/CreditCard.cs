using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.Models
{
    public class CreditCard : ObservableObject
    {
        private CreditCardType _type = CreditCardType.Mastercard;
        private string _owner;
        private string _number;
        private int? _cvv;
        private DateTime? _expiryDate;
        private bool _isDefault;
        private bool _isExpanded;

        public CreditCardType Type
        {
            get => _type;
            private set => SetProperty(ref _type, value);
        }
        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value?.Trim());
        }
        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value?.Trim(), onChanged: () => Type = GetType(_number));
        }
        public int? Cvv
        {
            get => _cvv;
            set => SetProperty(ref _cvv, value);
        }
        public DateTime? ExpiryDate
        {
            get => _expiryDate;
            set => SetProperty(ref _expiryDate, value);
        }
        public bool IsDefault
        {
            get => _isDefault;
            set => SetProperty(ref _isDefault, value);
        }
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public static CreditCardType GetType(string cardNumber)
        {
            CreditCardType result = CreditCardType.Mastercard;

            if (cardNumber?.Length > 0 && cardNumber[0] == '4')
                result = CreditCardType.Visa;

            return result;
        }

        public CreditCard Clone()
        {
            return (CreditCard)MemberwiseClone();
        }

        public void CopyFrom(CreditCard other)
        {
            Number = other.Number;
            Owner = other.Owner;
            Cvv = other.Cvv;
            ExpiryDate = other.ExpiryDate;
        }
    }
}
