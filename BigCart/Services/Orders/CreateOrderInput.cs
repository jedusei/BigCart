using BigCart.Models;
using System;

namespace BigCart.Services.Orders
{
    public record CreateOrderInput
    {
        public PaymentMethod PaymentMethod { get; }
        public CreditCardType? CreditCardType { get; }

        public CreateOrderInput(PaymentMethod paymentMethod, CreditCardType? creditCardType = null)
        {
            PaymentMethod = paymentMethod;
            if (paymentMethod == PaymentMethod.CreditCard)
            {
                if (creditCardType != null)
                    CreditCardType = creditCardType;
                else
                    throw new ArgumentNullException(nameof(creditCardType), $"Value cannot be null when {nameof(paymentMethod)} is {nameof(PaymentMethod)}.{nameof(PaymentMethod.CreditCard)}.");
            }
        }
    }
}
