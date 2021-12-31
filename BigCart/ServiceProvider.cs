using System;
using BigCart.Services.Address;
using BigCart.Services.Cart;
using BigCart.Services.CreditCards;
using BigCart.Services.Modal;
using BigCart.Services.Navigation;
using BigCart.Services.Orders;
using BigCart.Services.Pages;
using BigCart.Services.Products;
using BigCart.Services.Reviews;
using BigCart.Services.Transactions;
using BigCart.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BigCart
{
    public class ServiceProvider
    {
        private static IServiceProvider _provider;

        public static void Init(Action<IServiceCollection> configureServices = null)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            configureServices?.Invoke(services);

            _provider = services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _provider.GetRequiredService<T>();
        }

        public static object GetService(Type serviceType)
        {
            return _provider.GetRequiredService(serviceType);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAddressService, AddressService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<ICreditCardService, CreditCardService>();

            services.AddSingleton<IModalService, ModalService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IOrderService, OrderService>();

            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IReviewService, ReviewService>();
            services.AddSingleton<ITransactionService, TransactionService>();

            RegisterViewModels(services);
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<AboutViewModel>();
            services.AddTransient<AccountViewModel>();
            services.AddTransient<AddAddressViewModel>();

            services.AddTransient<AddCardViewModel>();
            services.AddTransient<AddReviewViewModel>();
            services.AddTransient<CartViewModel>();

            services.AddTransient<CategoriesViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<CheckoutViewModel>();

            services.AddTransient<FavoritesViewModel>();
            services.AddTransient<FilterViewModel>();
            services.AddTransient<ForgotPasswordViewModel>();

            services.AddTransient<HomeViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MyAddressViewModel>();

            services.AddTransient<MyCardsViewModel>();
            services.AddTransient<MyOrdersViewModel>();
            services.AddTransient<NotificationsViewModel>();

            services.AddTransient<OnboardingViewModel>();
            services.AddTransient<OrderSuccessViewModel>();
            services.AddTransient<ProductViewModel>();

            services.AddTransient<ReviewsViewModel>();
            services.AddTransient<SearchViewModel>();
            services.AddTransient<TrackOrderViewModel>();

            services.AddTransient<TransactionsViewModel>();
            services.AddTransient<VerifyNumberViewModel>();
            services.AddTransient<WelcomeViewModel>();
        }
    }
}
