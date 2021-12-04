using BigCart.Models;
using System;
using AddressModel = BigCart.Models.Address;

namespace BigCart.Services.Orders
{
    public record CreateOrderInput
    {
        public PaymentMethod PaymentMethod { get; }
        public AddressModel ShippingAddress { get; }
        public CreditCard CreditCard { get; }
        public DeliveryMethod DeliveryMethod { get; }

        public CreateOrderInput(PaymentMethod paymentMethod, AddressModel shippingAddress, CreditCard creditCard = null, DeliveryMethod deliveryMethod = DeliveryMethod.Standard)
        {
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;

            if (paymentMethod == PaymentMethod.CreditCard)
            {
                CreditCard = creditCard ??
                    throw new ArgumentNullException(nameof(creditCard), $"Value cannot be null when {nameof(paymentMethod)} is {nameof(PaymentMethod)}.{nameof(PaymentMethod.CreditCard)}.");
            }
        }
    }
}
