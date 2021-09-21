using System;

namespace BigCart.Models
{
    public enum CreditCardType
    {
        Mastercard,
        Visa,
        Paypal
    }

    public record Transaction
    {
        private static int _nextId = 123456;

        public int Id { get; } = _nextId++;
        public CreditCardType CreditCardType { get; init; }
        public float Amount { get; init; }
        public DateTime DateCreated { get; init; } = DateTime.Now;
    }
}
