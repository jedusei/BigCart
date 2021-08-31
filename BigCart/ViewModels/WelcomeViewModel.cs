namespace BigCart.ViewModels
{
    public class WelcomeViewModel : ViewModel
    {
        private int _currentPage;

        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

    }
}
