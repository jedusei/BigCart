using BigCart.Models;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public class AddReviewViewModel : ModalViewModel
    {
        private float _rating;
        private string _comment;

        public float Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value, onChanged: SubmitCommand.ChangeCanExecute);
        }
        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value?.Trim(' ', '\n'), onChanged: SubmitCommand.ChangeCanExecute);
        }
        public Command SubmitCommand { get; }

        public AddReviewViewModel()
        {
            SubmitCommand = new Command(Submit, () => (_rating > 0) && !string.IsNullOrEmpty(_comment));
        }

        private void Submit()
        {
            SetResult(new Review
            {
                User = new User { Name = "You", ProfilePicture = ImageSource.FromFile("avatar.png") },
                Rating = _rating,
                Comment = _comment
            });
        }
    }
}
