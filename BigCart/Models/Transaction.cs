using System;

namespace BigCart.Models
{
    public enum PaymentMethod
    {
        CreditCard,
        Paypal,
        ApplePay
    }

    public enum CreditCardType
    {
        Mastercard,
        Visa
    }

    public record Transaction
    {
        private static int _nextId = 123456;

        public int Id { get; } = _nextId++;
        public PaymentMethod PaymentMethod { get; init; }
        public CreditCardType? CreditCardType { get; init; }
        public float Amount { get; init; }
        public DateTime DateCreated { get; init; } = DateTime.Now;
    }
}
