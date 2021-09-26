using BigCart.Models;
using System;

namespace BigCart.Services.Transactions
{
    public record CreateTransactionInput
    {
        public PaymentMethod PaymentMethod { get; }
        public CreditCardType? CreditCardType { get; }
        public float Amount { get; }

        public CreateTransactionInput(float amount, PaymentMethod paymentMethod, CreditCardType? creditCardType = null)
        {
            Amount = amount;
            PaymentMethod = paymentMethod;
            if (paymentMethod == PaymentMethod.CreditCard)
            {
                CreditCardType = (creditCardType != null)
                    ? creditCardType
                    : throw new ArgumentNullException(nameof(creditCardType), $"Value cannot be null when {nameof(paymentMethod)} is {nameof(PaymentMethod)}.{nameof(PaymentMethod.CreditCard)}.");
            }
        }
    }
}
