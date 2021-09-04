using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class MainPage : TabbedPage
    {
        private List<Image> _tabIcons = new List<Image>();

        public MainPage()
        {
            InitializeComponent();
            GetTabIcons();
            _tabView.PropertyChanged += TabViewPropertyChanged;
        }

        protected override Tab[] GetTabs()
        {
            return _tabView.TabItems.Select(t => (Tab)t.Content).ToArray();
        }

        protected override int GetActiveTabIndex()
        {
            return _tabView.SelectedIndex;
        }

        private void GetTabIcons()
        {
            foreach (ContentView tabButton in _tabBar.Children)
            {
                if (tabButton.Style == _tabButtonStyle)
                    _tabIcons.Add((Image)tabButton.Content);
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

            OnSelectedTabChanged();
        }

    }
}