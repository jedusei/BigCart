using BigCart.Models;
using System;

namespace BigCart.Services.Orders
{
    public record CreateOrderInput
    {
        public PaymentMethod PaymentMethod { get; }
        public CreditCard CreditCard { get; }

        public CreateOrderInput(PaymentMethod paymentMethod, CreditCard creditCard = null)
        {
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
