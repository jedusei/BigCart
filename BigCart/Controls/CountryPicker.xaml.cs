using BigCart.Models;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.PopupLayout;
using System;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public partial class CountryPicker : SfBorder
    {
        public static readonly BindableProperty CountryProperty = BindableProperty.Create(
            nameof(Country),
            typeof(Country),
            typeof(CountryPicker),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                (bindable as CountryPicker).OnCountryChanged((Country)oldValue, (Country)newValue);
            }
        );

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(
                nameof(TextColor),
                typeof(Color),
                typeof(CountryPicker),
                defaultValueCreator: _ =>
                {
                    if (Application.Current.Resources.TryGetValue("TextPrimaryColor", out object value) && value is Color color)
                        return color;
                    else
                        return Color.Black;
                }
            );

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(
                nameof(PlaceholderColor),
                typeof(Color), typeof(CountryPicker),
                defaultValueCreator: _ =>
                {
                    if (Application.Current.Resources.TryGetValue("TextSecondaryColor", out object value) && value is Color color)
                        return color;
                    else
                        return Color.Gray;
                }
            );

        private readonly SfPopupLayout _popupLayout;
        private SfListView _countryListView;

        public Country Country
        {
            get => (Country)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public CountryPicker()
        {
            InitializeComponent();
            _popupLayout = new SfPopupLayout();
            _popupLayout.PopupView.ShowHeader = _popupLayout.PopupView.ShowFooter = false;
            _popupLayout.PopupView.BackgroundColor = Color.Transparent;
            _popupLayout.PopupView.PopupStyle.BorderThickness = 0;
            _popupLayout.PopupView.ContentTemplate = (DataTemplate)Resources["Popup"];
        }

        private void OnCountryChanged(Country oldValue, Country newValue)
        {
            if (oldValue == null || newValue == null)
            {
                _label.RemoveBinding(Label.TextColorProperty);
                if (newValue != null)
                    _label.SetBinding(Label.TextColorProperty, new Binding(TextColorProperty.PropertyName));
                else
                    _label.SetBinding(Label.TextColorProperty, new Binding(PlaceholderColorProperty.PropertyName));
            }
        }

        private void SfEffectsView_TouchUp(object sender, EventArgs e)
        {
            _popupLayout.Show(true);
        }

        private void CountryListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            _countryListView = (SfListView)sender;
            _countryListView.ItemsSource = Country.All;

            Country selectedCountry = Country;
            if (selectedCountry != null)
            {
                _countryListView.SelectedItem = selectedCountry;
                _countryListView.ScrollTo(selectedCountry, Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
            }
        }

        private void CountryListView_ItemClicked(object sender, EventArgs e)
        {
            var view = (View)sender;
            _countryListView.SelectedItem = view.BindingContext;
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            Country = (Country)_countryListView.SelectedItem;
            _popupLayout.Dismiss();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            _popupLayout.Dismiss();
        }
    }
}