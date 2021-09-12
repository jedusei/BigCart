using Foundation;
using UIKit;

namespace BigCart.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            IosModule.Instance.Initialize();

            global::Xamarin.Forms.Forms.Init();

            Syncfusion.XForms.iOS.ProgressBar.SfLinearProgressBarRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.EffectsView.SfEffectsViewRenderer.Init();
            Syncfusion.XForms.iOS.TextInputLayout.SfTextInputLayoutRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfSwitchRenderer.Init();
            Syncfusion.ListView.XForms.iOS.SfListViewRenderer.Init();
            _ = new Syncfusion.SfNumericTextBox.XForms.iOS.SfNumericTextBoxRenderer();

            LoadApplication(new App());

            Syncfusion.SfRating.XForms.iOS.SfRatingRenderer.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
