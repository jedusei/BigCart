using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BigCart.Controls
{
    public class DatePickerEx : DatePicker
    {
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(DatePickerEx), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(DatePickerEx));
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(DatePickerEx), Color.Gray);

        public DateTime? NullableDate
        {
            get => (DateTime?)GetValue(NullableDateProperty);
            set => SetValue(NullableDateProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public DatePickerEx()
        {
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == DateProperty.PropertyName)
                NullableDate = Date;
            else if (propertyName == NullableDateProperty.PropertyName)
            {
                if (NullableDate.HasValue)
                    Date = NullableDate.Value;
            }
        }
    }
}
