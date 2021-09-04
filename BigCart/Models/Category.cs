using Xamarin.Forms;
using AppIcons = BigCart.Icons.Icons;

namespace BigCart.Models
{
    public class Category
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public string IconGlyph { get; set; }

        public static readonly Category[] All = new Category[]
        {
            new()
            {
                Name = "Vegetables",
                BackgroundColor = Color.FromHex("#E6F2EA"),
                ForegroundColor = Color.FromHex("#28B446"),
                IconGlyph = AppIcons.Lettuce
            },
            new()
            {
                Name = "Fruits",
                BackgroundColor = Color.FromHex("#FFE9E5"),
                ForegroundColor = Color.FromHex("#F8644A"),
                IconGlyph = AppIcons.Apple
            },
            new()
            {
                Name = "Beverages",
                BackgroundColor = Color.FromHex("#FFF6E3"),
                ForegroundColor = Color.FromHex("#F5BA3C"),
                IconGlyph = AppIcons.Beverage
            },
            new()
            {
                Name = "Grocery",
                BackgroundColor = Color.FromHex("#F3EFFA"),
                ForegroundColor = Color.FromHex("#AE80FF"),
                IconGlyph = AppIcons.Grocery
            },
            new()
            {
                Name = "Edible Oil",
                BackgroundColor = Color.FromHex("#DCF4F5"),
                ForegroundColor = Color.FromHex("#0CD4DC"),
                IconGlyph = AppIcons.Oil
            },
            new()
            {
                Name = "Household",
                BackgroundColor = Color.FromHex("#FFE8F2"),
                ForegroundColor = Color.FromHex("#FF7EB6"),
                IconGlyph = AppIcons.Vacuum
            },
            new()
            {
                Name = "Babycare",
                BackgroundColor = Color.FromHex("#D2EFFF"),
                ForegroundColor = Color.FromHex("#1BADFF"),
                IconGlyph = AppIcons.Baby
            },
        };
    }
}
