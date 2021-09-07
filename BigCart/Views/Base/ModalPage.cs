using System;
using System.Collections.Generic;
using System.Text;

namespace BigCart.Pages
{
    public abstract class ModalPage : Page
    {
        protected override IReadOnlyList<Xamarin.Forms.Page> GetNavigationStack()
        {
            return Navigation.ModalStack;
        }
    }
}
