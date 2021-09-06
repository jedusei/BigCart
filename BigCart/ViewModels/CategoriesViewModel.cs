using BigCart.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class CategoriesViewModel : ViewModel
    {
        public ICommand ViewCategoryCommand { get; }

        public CategoriesViewModel()
        {
            ViewCategoryCommand = new Command<Category>(c => _modalService.AlertAsync("Details", c.Name));
        }
    }
}
