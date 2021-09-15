using Android.Content;
using BigCart.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePickerEx), typeof(BigCart.Droid.Renderers.DatePickerExRenderer))]
namespace BigCart.Droid.Renderers
{
    public class DatePickerExRenderer : DatePickerRenderer
    {
        private new DatePickerEx Element => base.Element as DatePickerEx;

        public DatePickerExRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                SetNullableDate();
                SetPlaceholder();
                SetPlaceholderColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DatePickerEx.NullableDateProperty.PropertyName)
                SetNullableDate();
            else if (e.PropertyName == DatePickerEx.PlaceholderProperty.PropertyName)
                SetPlaceholder();
            else if (e.PropertyName == DatePickerEx.PlaceholderColorProperty.PropertyName)
                SetPlaceholderColor();
        }

        private void SetNullableDate()
        {
            if (!Element.NullableDate.HasValue)
                Control.Text = null;
        }

        private void SetPlaceholder()
        {
            Control.Hint = Element.Placeholder;
        }

        private void SetPlaceholderColor()
        {
            Control.SetHintTextColor(Element.PlaceholderColor.ToAndroid());
        }
    }
}