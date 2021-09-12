namespace BigCart.Pages
{
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            base.OnStart();
            _txtSearch.Focus();
        }
    }
}