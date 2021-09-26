using BigCart.Models;
using BigCart.ViewModels;
using Syncfusion.XForms.ProgressBar;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.Forms;
using AppIcons = BigCart.Icons.Icons;

namespace BigCart.Pages
{
    public partial class TrackOrderPage : Page
    {
        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(TrackOrderPage));

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            private set => SetValue(ProgressProperty, value);
        }

        public TrackOrderPage()
        {
            InitializeComponent();
            _progressBar.PropertyChanged += ProgressBar_PropertyChanged;
            BindableLayout.SetItemsSource(_orderStepsLayout, new OrderStep[] {
                new()
                {
                    Title = "Order Placed",
                    BindingPath = nameof(Order.CreatedAt),
                    Progress = 0,
                    IconGlyph = AppIcons.BoxOpen,
                    IconHeight = 35
                },
                new()
                {
                    Title = "Order Confirmed",
                    BindingPath = nameof(Order.ConfirmedAt),
                    Progress = 25,
                    IconGlyph = AppIcons.TickCircle,
                    IconHeight = 35
                },
                new()
                {
                    Title = "Order Shipped",
                    BindingPath = nameof(Order.ShippedAt),
                    Progress = 50,
                    IconGlyph = AppIcons.MapNavigation,
                    IconHeight = 50
                },
                new()
                {
                    Title = "Out for Delivery",
                    BindingPath = nameof(Order.OutForDeliveryAt),
                    Progress = 75,
                    IconGlyph = AppIcons.Shipping,
                    IconHeight = 35
                },
                new()
                {
                    Title = "Order Delivered",
                    BindingPath = nameof(Order.DeliveredAt),
                    Progress = 100,
                    IconGlyph = AppIcons.BoxCart,
                    IconHeight = 40,
                    ShowSeparator = false
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Status == PageStatus.Pending)
            {
                SetupStepBindings();
                PositionProgressBar();
            }
        }

        private void ProgressBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ProgressBarBase.ActualProgressValueProperty.PropertyName)
                SetValue(ProgressProperty, _progressBar.GetValue(ProgressBarBase.ActualProgressValueProperty));
        }

        private void SetupStepBindings()
        {
            var viewModel = (TrackOrderViewModel)BindingContext;
            foreach (View view in _orderStepsLayout.Children)
            {
                var step = (OrderStep)view.BindingContext;

                View border = view.FindByName<View>("border");
                var trigger = (DataTrigger)border.Resources["trigger"];
                trigger.Binding = new Binding
                {
                    Path = ProgressProperty.PropertyName,
                    Source = this,
                    Converter = new CompareConverter
                    {
                        ComparisonOperator = CompareConverter<object>.OperatorType.GreaterOrEqual,
                        ComparingValue = step.Progress
                    }
                };
                border.Triggers.Add(trigger);

                Label txtDate = view.FindByName<Label>("txtDate");
                txtDate.SetBinding(Label.TextProperty, new Binding
                {
                    Path = $"{nameof(TrackOrderViewModel.Order)}.{step.BindingPath}",
                    StringFormat = "{0:MMMM dd yyyy}",
                    Source = viewModel
                });
            }
        }

        private void PositionProgressBar()
        {
            var layoutRoot = (Layout)_orderStepsLayout.Parent;

            var firstIcon = (View)_orderStepsLayout.Children[0].FindByName("icon");
            var lastIcon = (View)_orderStepsLayout.Children[^1].FindByName("icon");

            Point startPosition = firstIcon.GetRelativePosition(layoutRoot);
            Point endPosition = lastIcon.GetRelativePosition(layoutRoot);

            _progressBar.TranslationX = startPosition.X + (_progressBar.Height * 0.5f);
            _progressBar.TranslationY = startPosition.Y;

            _progressBar.WidthRequest = endPosition.Y - startPosition.Y;
        }

        private class OrderStep
        {
            public string Title { get; set; }
            public string BindingPath { get; set; }
            public double Progress { get; set; }
            public float IconHeight { get; set; }
            public string IconGlyph { get; set; }
            public bool ShowSeparator { get; set; } = true;
        }
    }
}