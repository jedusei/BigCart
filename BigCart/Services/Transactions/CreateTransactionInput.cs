using BigCart.Models;
using System;

namespace BigCart.Services.Transactions
{
    public record CreateTransactionInput
    {
        public PaymentMethod PaymentMethod { get; }
        public CreditCard CreditCard { get; }
        public float Amount { get; }

        public CreateTransactionInput(float amount, PaymentMethod paymentMethod, CreditCard creditCard = null)
        {
            Amount = amount;
            PaymentMethod = paymentMethod;
            if (paymentMethod == PaymentMethod.CreditCard)
            {
                CreditCard = (creditCard != null)
                    ? creditCard
                    : throw new ArgumentNullException(nameof(creditCard), $"Value cannot be null when {nameof(paymentMethod)} is {nameof(PaymentMethod)}.{nameof(PaymentMethod.CreditCard)}.");
            }
        }
    }
}
