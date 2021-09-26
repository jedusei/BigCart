using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.Models
{
    public class Order : ObservableObject
    {
        private float _progress;
        private DateTime? _confirmedAt, _shippedAt, _outForDeliveryAt, _deliveredAt;
        private bool _isExpanded;

        public int Id { get; init; }
        public int ItemCount { get; init; }
        public float Cost { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public float Progress
        {
            get => _progress;
            private set => SetProperty(ref _progress, value, onChanged: UpdateProgress);
        }
        public DateTime? ConfirmedAt
        {
            get => _confirmedAt;
            set => SetProperty(ref _confirmedAt, value, onChanged: UpdateProgress);
        }
        public DateTime? ShippedAt
        {
            get => _shippedAt;
            set => SetProperty(ref _shippedAt, value, onChanged: UpdateProgress);
        }
        public DateTime? OutForDeliveryAt
        {
            get => _outForDeliveryAt;
            set => SetProperty(ref _outForDeliveryAt, value, onChanged: UpdateProgress);
        }
        public DateTime? DeliveredAt
        {
            get => _deliveredAt;
            set => SetProperty(ref _deliveredAt, value, onChanged: UpdateProgress);
        }
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        private void UpdateProgress()
        {
            if (_deliveredAt.HasValue)
                Progress = 100;
            else if (_outForDeliveryAt.HasValue)
                Progress = 75;
            else if (_shippedAt.HasValue)
                Progress = 50;
            else if (_confirmedAt.HasValue)
                Progress = 25;
            else
                Progress = 0;
        }
    }
}
