using BigCart.Models;
using BigCart.Pages;
using BigCart.Services.Navigation;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BigCart.ViewModels
{
    public class CategoriesViewModel : ViewModel
    {
        public ICommand ViewCategoryCommand { get; }

        public CategoriesViewModel()
        {
            ViewCategoryCommand = new AsyncCommand<Category>(category => _navigationService.PushAsync<CategoryPage>(new NavigationOptions { Data = category }));
        }
    }
}
