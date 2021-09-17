using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.Models
{
    public class Order : ObservableObject
    {
        private DateTime? _confirmedAt, _shippedAt, _outForDeliveryAt, _deliveredAt;

        public int Id { get; }
        public int ItemCount { get; }
        public float Cost { get; }
        public DateTime CreatedAt { get; }
        public DateTime? ConfirmedAt
        {
            get => _confirmedAt;
            set => SetProperty(ref _confirmedAt, value);
        }
        public DateTime? ShippedAt
        {
            get => _shippedAt;
            set => SetProperty(ref _shippedAt, value);
        }
        public DateTime? OutForDeliveryAt
        {
            get => _outForDeliveryAt;
            set => SetProperty(ref _outForDeliveryAt, value);
        }
        public DateTime? DeliveredAt
        {
            get => _deliveredAt;
            set => SetProperty(ref _deliveredAt, value);
        }

        public Order(int id, int itemCount, float cost)
        {
            Id = id;
            ItemCount = itemCount;
            Cost = cost;
            CreatedAt = DateTime.Now;
        }
    }
}
