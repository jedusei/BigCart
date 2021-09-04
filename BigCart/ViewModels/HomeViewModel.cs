namespace BigCart.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        static int nextId = 1;
        readonly int Id;

        public HomeViewModel()
        {
            Id = nextId++;
        }
    }
}
