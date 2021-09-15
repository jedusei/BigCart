namespace BigCart.ViewModels
{
    public class OrderSuccessViewModel : ViewModel
    {
        public override bool OnBackButtonPressed()
        {
            _ = _navigationService.PopToRootAsync();
            return true;
        }
    }
}
