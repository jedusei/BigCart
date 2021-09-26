using BigCart.Models;
using Syncfusion.ListView.XForms;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class MyOrdersPage : Page
    {
        private static readonly OrderStep[] _steps = new OrderStep[] {
            new()
            {
                Title = "Order Placed",
                BindingPath = nameof(Order.CreatedAt),
                Progress = 0
            },
            new()
            {
                Title = "Order Confirmed",
                BindingPath = nameof(Order.ConfirmedAt),
                Progress = 25
            },
            new()
            {
                Title = "Order Shipped",
                BindingPath = nameof(Order.ShippedAt),
                Progress = 50
            },
            new()
            {
                Title = "Out for Delivery",
                BindingPath = nameof(Order.OutForDeliveryAt),
                Progress = 75
            },
            new()
            {
                Title = "Order Delivered",
                BindingPath = nameof(Order.DeliveredAt),
                Progress = 100
            }
        };

        public MyOrdersPage()
        {
            InitializeComponent();
            _listView.ItemGenerator = new ItemGeneratorEx(_listView);
        }

        private class OrderStep
        {
            public string Title { get; set; }
            public string BindingPath { get; set; }
            public float Progress { get; set; }
        }

        private class ItemGeneratorEx : ItemGenerator
        {
            public ItemGeneratorEx(SfListView listView) : base(listView)
            {
            }

            protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
            {
                if (type == ItemType.Record)
                    return new ListViewItemEx();

                return base.OnCreateListViewItem(itemIndex, type, data);
            }
        }

        private class ListViewItemEx : ListViewItem
        {
            private Expander _expander;
            private View _dropdownIcon;
            private StackLayout _stepsLayout;
            private View _progressBar;

            protected override void OnChildAdded(Element child)
            {
                base.OnChildAdded(child);
                _expander = child as Expander;
                _expander.Tapped += Expander_Tapped;
                _dropdownIcon = _expander.FindByName<View>("dropdownIcon");
                SetupSteps();
            }

            protected override void OnSizeAllocated(double width, double height)
            {
                base.OnSizeAllocated(width, height);
                if (width != -1 && height != -1)
                    PositionProgressBar();
            }

            private void Expander_Tapped(object sender, System.EventArgs e)
            {
                _dropdownIcon.CancelAnimations();
                _dropdownIcon.RotateTo(_expander.IsExpanded ? -180 : 0, _expander.AnimationLength, _expander.AnimationEasing);
            }

            private void SetupSteps()
            {
                _stepsLayout = Content.FindByName<StackLayout>("stepsLayout");
                BindableLayout.SetItemsSource(_stepsLayout, _steps);

                RelativeBindingSource bindingSource = new(RelativeBindingSourceMode.FindAncestorBindingContext, typeof(Order));

                foreach (View stepView in _stepsLayout.Children)
                {
                    var step = (OrderStep)stepView.BindingContext;

                    var txtDate = stepView.FindByName<Label>("txtDate");
                    txtDate.SetBinding(Label.TextProperty, new Binding
                    {
                        Path = step.BindingPath,
                        StringFormat = "{0:MMM dd yyyy}",
                        Source = bindingSource
                    });

                    var txtPending = stepView.FindByName<Label>("txtPending");
                    txtPending.SetBinding(IsVisibleProperty, new Binding
                    {
                        Path = nameof(Order.Progress),
                        Source = bindingSource,
                        Converter = new CompareConverter
                        {
                            ComparisonOperator = CompareConverter<object>.OperatorType.Smaller,
                            ComparingValue = step.Progress
                        }
                    });
                }
            }

            private void PositionProgressBar()
            {
                var layoutRoot = (Layout)_stepsLayout.Parent;
                _progressBar = layoutRoot.FindByName<View>("progressBar");

                var firstEllipse = _stepsLayout.Children[0].FindByName<View>("ellipse");
                var lastEllipse = _stepsLayout.Children[^1].FindByName<View>("ellipse");

                Point startPosition = firstEllipse.GetRelativePosition(layoutRoot);
                Point endPosition = lastEllipse.GetRelativePosition(layoutRoot);

                _progressBar.TranslationX = startPosition.X + (_progressBar.Height * 0.5f);
                _progressBar.TranslationY = startPosition.Y;

                _progressBar.WidthRequest = endPosition.Y - startPosition.Y;
            }
        }
    }
}