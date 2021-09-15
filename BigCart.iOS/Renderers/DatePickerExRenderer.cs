using BigCart.Controls;
using Foundation;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerEx), typeof(BigCart.iOS.Renderers.DatePickerExRenderer))]
namespace BigCart.iOS.Renderers
{
    public class DatePickerExRenderer : DatePickerRenderer
    {
        private new DatePickerEx Element => base.Element as DatePickerEx;

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                SetNullableDate();
                SetPlaceholder();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DatePickerEx.NullableDateProperty.PropertyName)
                SetNullableDate();
            else if (e.PropertyName == DatePickerEx.PlaceholderProperty.PropertyName || e.PropertyName == DatePickerEx.PlaceholderColorProperty.PropertyName)
                SetPlaceholder();
        }

        private void SetNullableDate()
        {
            if (!Element.NullableDate.HasValue)
                Control.Text = null;
        }

        private void SetPlaceholder()
        {
            Control.AttributedPlaceholder = new NSAttributedString(Element.Placeholder, Control.Font, Element.PlaceholderColor.ToUIColor());
        }
    }
}