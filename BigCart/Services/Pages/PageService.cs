using BigCart.ViewModels;
using System;
using Xamarin.Forms;

namespace BigCart.Services.Pages
{
    public class PageService : IPageService
    {
        public Page MainPage
        {
            get => Application.Current.MainPage;
            set => Application.Current.MainPage = value;
        }

        public T CreatePage<T>(object navigationData = null) where T : Page
        {
            T page = Activator.CreateInstance<T>();
            if (page.BindingContext is ViewModel viewModel)
                viewModel.Initialize(navigationData);

            return page;
        }
    }
}
