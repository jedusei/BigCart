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
        public int Id { get; init; }
        public float Amount { get; init; }
        public PaymentMethod PaymentMethod { get; init; }
        public CreditCardType? CreditCardType { get; init; }
        public DateTime DateCreated { get; init; } = DateTime.Now;
    }
}
