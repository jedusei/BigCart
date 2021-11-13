using Acr.UserDialogs;
using BigCart.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class VerifyNumberViewModel : ViewModel
    {
        private const string VERIFICATION_CODE = "123456";
        private static readonly TimeSpan WAIT_TIME_INCREMENT = TimeSpan.FromSeconds(30);
        private int _currentStep;
        private string _phoneNumber;
        private string _code;
        private TimeSpan _waitTime;
        private TimeSpan _nextWaitTime = WAIT_TIME_INCREMENT;
        private CancellationTokenSource _timerCts;

        public int CurrentStep
        {
            get => _currentStep;
            set => SetProperty(ref _currentStep, value);
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value?.Trim(), onChanged: SendCodeCommand.RaiseCanExecuteChanged);
        }
        public string Code
        {
            get => _code;
            set => SetProperty(ref _code, value, onChanged: VerifyCodeCommand.RaiseCanExecuteChanged);
        }
        public TimeSpan WaitTime
        {
            get => _waitTime;
            set => SetProperty(ref _waitTime, value);
        }
        public AsyncCommand SendCodeCommand { get; }
        public AsyncCommand ResendCodeCommand { get; }
        public AsyncCommand VerifyCodeCommand { get; }

        public VerifyNumberViewModel()
        {
            SendCodeCommand = new AsyncCommand(() => SendCodeAsync(false), _ => _phoneNumber != null && _phoneNumber.Length > 10 && _phoneNumber.Length <= 16, allowsMultipleExecutions: false);
            ResendCodeCommand = new AsyncCommand(() => SendCodeAsync(true), _ => _waitTime.TotalSeconds == 0, allowsMultipleExecutions: false);
            VerifyCodeCommand = new AsyncCommand(VerifyCodeAsync, _ => _code?.Length == AppConsts.OTP_LENGTH, allowsMultipleExecutions: false);
        }

        public override bool OnBackButtonPressed()
        {
            if (_currentStep > 0)
            {
                CurrentStep--;
                return true;
            }

            return base.OnBackButtonPressed();
        }

        public override void OnStop()
        {
            base.OnStop();
            _timerCts?.Cancel();
        }

        private async Task SendCodeAsync(bool isResend)
        {
            _modalService.ShowLoading("Sending code...");
            await Task.Delay(1000);
            _modalService.HideLoading();

            WaitTime = _nextWaitTime;
            _nextWaitTime += WAIT_TIME_INCREMENT;
            ResendCodeCommand.RaiseCanExecuteChanged();

            _timerCts?.Cancel();
            var timerCts = _timerCts = new CancellationTokenSource();

            TimeSpan interval = TimeSpan.FromSeconds(1);
            Device.StartTimer(interval, () =>
            {
                if (timerCts.IsCancellationRequested)
                    return false;

                if (WaitTime.TotalSeconds != 0)
                {
                    Device.BeginInvokeOnMainThread(() => WaitTime -= interval);
                    return true;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => ResendCodeCommand.RaiseCanExecuteChanged());
                    return false;
                }
            });

            UserDialogs.Instance.Toast(new($"Your verification code is {VERIFICATION_CODE}.")
            {
                Position = ToastPosition.Top
            });

            if (!isResend)
                CurrentStep++;
        }

        private async Task VerifyCodeAsync()
        {
            _modalService.ShowLoading("Verifying code...");
            await Task.Delay(1000);

            if (Code != VERIFICATION_CODE)
            {
                _modalService.HideLoading();
                await _modalService.AlertAsync("The verification code you entered is invalid.", "Invalid Code");
            }
            else
            {
                _modalService.ShowLoading("Creating account...");
                await Task.Delay(1000);
                _modalService.HideLoading();

                await _navigationService.PushAsync<MainPage>(new() { ClearHistory = true });
            }
        }
    }
}
