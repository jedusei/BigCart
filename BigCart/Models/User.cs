using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BigCart.Models
{
    public class User : ObservableObject
    {
        private string _name;
        private ImageSource _profilePicture;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public ImageSource ProfilePicture
        {
            get => _profilePicture;
            set => SetProperty(ref _profilePicture, value);
        }
    }
}
