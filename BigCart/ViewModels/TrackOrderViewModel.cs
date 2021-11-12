using BigCart.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BigCart.ViewModels
{
    public class TrackOrderViewModel : ViewModel
    {
        private const int DELAY_DURATION = 1000;
        private readonly CancellationTokenSource _cts = new();

        public Order Order { get; private set; }

        public override void Initialize(object navigationData)
        {
            base.Initialize(navigationData);
            Order = (Order)navigationData;
            OnPropertyChanged(nameof(Order));
        }

        public override void OnStart()
        {
            base.OnStart();
            _ = UpdateProgressAsync();
        }

        private async Task UpdateProgressAsync()
        {
            await Delay();

            Order.ConfirmedAt = DateTime.Now;

            await Delay();

            Order.ShippedAt = DateTime.Now;

            await Delay();

            Order.OutForDeliveryAt = DateTime.Now;

            await Delay();

            Order.DeliveredAt = DateTime.Now;
        }

        public override bool OnBackButtonPressed()
        {
            _ = _navigationService.PopToRootAsync();
            return true;
        }

        public override void OnStop()
        {
            base.OnStop();
            _cts.Cancel();
        }

        private Task Delay()
        {
            return Task.Delay(DELAY_DURATION, _cts.Token);
        }
    }
}
