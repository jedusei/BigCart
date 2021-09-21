using BigCart.DependencyInjection;
using BigCart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigCart.Services.Reviews
{
    public class ReviewService : IReviewService, ISingletonDependency
    {
        private static Dictionary<Product, ObservableCollection<Review>> _reviews = new();
        private static Review[] _defaultReviews;

        public async Task<ReadOnlyObservableCollection<Review>> GetReviewsAsync(Product product)
        {
            if (product == null)
                return null;

            await Task.Delay(1000);
            return new ReadOnlyObservableCollection<Review>(GetProductReviewList(product));
        }

        public async Task AddReviewAsync(Product product, Review review)
        {
            await Task.Delay(1000);
            ObservableCollection<Review> reviews = GetProductReviewList(product);
            reviews.Insert(0, review);
            product.ReviewCount = reviews.Count;
        }

        private ObservableCollection<Review> GetProductReviewList(Product product)
        {
            if (!_reviews.TryGetValue(product, out ObservableCollection<Review> reviews))
            {
                InitDefaultReviews();
                reviews = new(_defaultReviews);
                _reviews.Add(product, reviews);
            }

            return reviews;
        }

        private void InitDefaultReviews()
        {
            if (_defaultReviews != null)
                return;

            var dummyComment = (string)Application.Current.Resources["LoremIpsum"];

            _defaultReviews = new Review[]
            {
                new()
                {
                   User = new User
                   {
                       Name = "Haylie Aminoff",
                      ProfilePicture = ImageSource.FromFile("user_haylie.png")
                   },
                   Rating = 4.5f,
                   Comment = dummyComment,
                   DateCreated = DateTime.Now.AddMinutes(-20)
                },
                new()
                {
                   User = new User
                   {
                       Name = "Carla Septimus",
                      ProfilePicture = ImageSource.FromFile("user_carla_s.png")
                   },
                   Rating = 4.5f,
                   Comment = dummyComment,
                   DateCreated = DateTime.Now.AddMinutes(-25)
                },
                new()
                {
                   User = new User
                   {
                       Name = "Carla George",
                      ProfilePicture = ImageSource.FromFile("user_carla_g.png")
                   },
                   Rating = 4.5f,
                   Comment = dummyComment,
                   DateCreated = DateTime.Now.AddMinutes(-30)
                },
                new()
                {
                   User = new User
                   {
                       Name = "Maren Kenter",
                      ProfilePicture = ImageSource.FromFile("user_maren.png")
                   },
                   Rating = 4.5f,
                   Comment = dummyComment,
                   DateCreated = DateTime.Now.AddMinutes(-32)
                }
            };
        }
    }
}
