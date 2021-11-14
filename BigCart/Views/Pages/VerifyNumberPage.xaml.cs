using BigCart.Models;
using BigCart.ViewModels;
using Newtonsoft.Json;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class VerifyNumberPage : Page
    {
        private readonly Country[] _countries;
        private readonly SfPopupLayout _popupLayout;
        private VerifyNumberViewModel _viewModel;
        private SfListView _countryListView;
        private Country _selectedCountry;

        public VerifyNumberPage()
        {
            InitializeComponent();

            // Get countries
            using System.IO.StreamReader reader = new(GetType().Assembly.GetManifestResourceStream("BigCart.Assets.countries.json"));
            string json = reader.ReadToEnd();
            _countries = JsonConvert.DeserializeObject<Country[]>(json);
            SelectCountry(_countries.First(c => c.NameCode == "USA"));

            // Setup popup
            _popupLayout = new SfPopupLayout();
            _popupLayout.PopupView.ShowHeader = _popupLayout.PopupView.ShowFooter = false;
            _popupLayout.PopupView.BackgroundColor = Color.Transparent;
            _popupLayout.PopupView.PopupStyle.BorderThickness = 0;
            _popupLayout.PopupView.ContentTemplate = (DataTemplate)Resources["Popup"];
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = (VerifyNumberViewModel)BindingContext;
        }

        private void CountryButton_Clicked(object sender, System.EventArgs e)
        {
            _popupLayout.Show(true);
        }

        private void CountryListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            _countryListView = (SfListView)sender;
            _countryListView.ItemsSource = _countries;
            _countryListView.SelectedItem = _selectedCountry;
            _countryListView.ScrollTo(_selectedCountry, Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
        }

        private void CountryListView_ItemClicked(object sender, EventArgs e)
        {
            var view = (View)sender;
            _countryListView.SelectedItem = view.BindingContext;
        }

        private void SelectCountry(Country country)
        {
            _selectedCountry = country;
            _countryCodeLabel.Text = _selectedCountry.CallingCode;
            _flagImage.Source = _selectedCountry.FlagUrl;
            UpdatePhoneNumber();
        }

        private void PhoneNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            _phoneNumberEntry.Text = e.NewTextValue.Trim();
            UpdatePhoneNumber();
        }

        private void UpdatePhoneNumber()
        {
            _viewModel.PhoneNumber = _countryCodeLabel.Text + _phoneNumberEntry.Text?.TrimStart(' ', '0').TrimEnd();
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            SelectCountry((Country)_countryListView.SelectedItem);
            ClosePopup();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void ClosePopup()
        {
            _popupLayout.Dismiss();
            _countryListView = null;
        }

        private async void TabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TabView.SelectedIndexProperty.PropertyName)
            {
                await Task.Delay(300);
                if (_tabView.SelectedIndex == 1)
                    _codeView.Focus();
            }
        }
    }
}