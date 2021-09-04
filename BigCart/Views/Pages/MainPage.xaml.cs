using System.Collections.Generic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class MainPage : Page
    {
        private List<Image> _tabIcons = new List<Image>();

        public MainPage()
        {
            InitializeComponent();
            GetTabIcons();
            _tabView.PropertyChanged += TabViewPropertyChanged;
        }

        private void GetTabIcons()
        {
            foreach (ContentView tab in _tabBar.Children)
            {
                if (tab.Style == _tabButtonStyle)
                    _tabIcons.Add(tab.Content as Image);
            }
        }

        private void TabViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TabView.SelectedIndexProperty.PropertyName)
                OnTabViewSelectedIndexChanged();
        }

        private void OnTabViewSelectedIndexChanged()
        {
            var defaultColor = (Color)App.Current.Resources["TextSecondaryColor"];
            var activeColor = (Color)App.Current.Resources["TextPrimaryColor"];

            for (int i = 0; i < _tabIcons.Count; i++)
            {
                var imageSource = (FontImageSource)_tabIcons[i].Source;
                imageSource.Color = (i == _tabView.SelectedIndex) ? activeColor : defaultColor;
            }
        }
    }
}