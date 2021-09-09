using Xamarin.Forms;
using AppIcons = BigCart.Icons.Icons;

namespace BigCart.Pages
{
    public partial class AccountTab : Tab
    {
        public const string MENU_ITEM_ABOUT = "About";
        public const string MENU_ITEM_ORDERS = "Orders";
        public const string MENU_ITEM_FAVORITES = "Favorites";
        public const string MENU_ITEM_ADDRESS = "Address";
        public const string MENU_ITEM_CARDS = "Cards";
        public const string MENU_ITEM_TRANSACTIONS = "Transactions";
        public const string MENU_ITEM_NOTIFICATIONS = "Notifications";
        public const string MENU_ITEM_LOGOUT = "Sign out";

        public AccountTab()
        {
            InitializeComponent();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            var iconFontFamily = Application.Current.Resources["Icons"] as string;
            var iconColor = (Color)Resources["MenuIconColor"];
            var menuItems = new[]
            {
                new
                {
                    Id = MENU_ITEM_ABOUT,
                    Title = "About me",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.User,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_ORDERS,
                    Title = "My Orders",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.Box,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_FAVORITES,
                    Title = "My Favorites",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.Heart,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_ADDRESS,
                    Title = "My Address",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.MapMarker,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_CARDS,
                    Title = "Credit Cards",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.CreditCard,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_TRANSACTIONS,
                    Title = "Transactions",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.Transactions,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_NOTIFICATIONS,
                    Title = "Notifications",
                    ShowArrow = true,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.Notifications,
                        Color = iconColor
                    }
                },
                new
                {
                    Id = MENU_ITEM_LOGOUT,
                    Title = "Sign out",
                    ShowArrow = false,
                    Icon = new FontImageSource
                    {
                        FontFamily = iconFontFamily,
                        Glyph = AppIcons.SignOut,
                        Color = iconColor
                    }
                },
           };

            BindableLayout.SetItemsSource(_menuLayout, menuItems);
        }
    }
}