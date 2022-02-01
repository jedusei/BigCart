using Xamarin.UITest;

namespace BigCart.UITests.Views
{
    public abstract class View
    {
        protected readonly IApp _app;
        protected readonly string _trait;

        public View(string trait = null)
        {
            _app = AppInitializer.App;
            _trait = trait ?? GetType().Name;
            _app.WaitForElement(_trait);
        }

        public void WaitUntilHidden()
        {
            _app.WaitForNoElement(_trait);
        }
    }
}
