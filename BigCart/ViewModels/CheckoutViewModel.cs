using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class CheckoutViewModel : ViewModel
    {
        private int _currentStep;
        private bool _isComplete;

        public string Title { get; private set; }
        public int CurrentStep
        {
            get => _currentStep;
            set => SetProperty(ref _currentStep, value, onChanged: UpdateTitle);
        }
        public bool IsComplete
        {
            get => _isComplete;
            set => SetProperty(ref _isComplete, value);
        }
        public ICommand NextStepCommand { get; }

        public CheckoutViewModel()
        {
            UpdateTitle();
            NextStepCommand = new AsyncCommand(NextStepAsync, allowsMultipleExecutions: false);
        }

        private void UpdateTitle()
        {
            Title = _currentStep switch
            {
                0 => "Shipping Method",
                1 => "Shipping Address",
                _ => "Payment Method"
            };

            OnPropertyChanged(nameof(Title));
        }

        private async Task NextStepAsync()
        {
            if (_currentStep < 2)
                CurrentStep++;
            else
            {
                IsComplete = true;
                _modalService.ShowLoading("Making payment...");
                await Task.Delay(1000);
                _modalService.HideLoading();
            }
        }
    }
}
