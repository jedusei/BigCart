using System.Collections.Generic;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace BigCart.Controls
{
    [ContentProperty(nameof(Items))]
    public partial class AutoSizeCarouselView : ContentView
    {
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(int), typeof(AutoSizeCarouselView), propertyChanged: PositionChanged);
        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(uint), typeof(AutoSizeCarouselView), 300u);
        public static readonly BindableProperty EasingProperty = BindableProperty.Create(nameof(Easing), typeof(Easing), typeof(AutoSizeCarouselView), Easing.CubicInOut);

        private const string ANIM_RESIZE = "resize";
        private const string ANIM_SLIDE = "slide";
        private Layout<View> _rootLayout;
        private IList<View> _children;

        public IList<View> Items { get; } = new List<View>();
        public int Position
        {
            get => (int)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }
        public uint Duration
        {
            get => (uint)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }
        public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        public AutoSizeCarouselView()
        {
            InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _rootLayout = (Layout<View>)GetTemplateChild("rootLayout");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (_children != null)
            {
                foreach (ContentView child in _children)
                    child.Content.BindingContext = BindingContext;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (_rootLayout != null && _children == null)
            {
                BindableLayout.SetItemsSource(_rootLayout, Items);
                _children = _rootLayout.Children;
                View activeChild = _children[Position];
                HeightRequest = activeChild.Measure(Width, Height).Request.Height;

                foreach (ContentView child in _children)
                {
                    child.Content.BindingContext = BindingContext;
                    if (child != activeChild)
                        child.TranslationY = 10000;
                }
            }
        }

        private static void PositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var carousel = (AutoSizeCarouselView)bindable;
            carousel.OnPositionChanged((int)oldValue, (int)newValue);
        }

        private void OnPositionChanged(int oldPosition, int newPosition)
        {
            if (_children == null)
                return;

            View previousChild = _children[oldPosition];
            View nextChild = _children[newPosition];

            this.AbortAnimations(ANIM_RESIZE, ANIM_SLIDE);

            double startX = (newPosition > oldPosition) ? previousChild.Width : -previousChild.Width;
            double endX = -startX;
            nextChild.TranslationX = startX;
            nextChild.TranslationY = 0;

            var resizeAnimation = new Animation(h => HeightRequest = h, Height, nextChild.Height);
            var slideAnimation = new Animation(
                x =>
                {
                    previousChild.TranslationX = x;
                    nextChild.TranslationX = startX + x;
                },
                start: 0,
                end: endX
            );

            resizeAnimation.Commit(this, ANIM_RESIZE, length: Duration, easing: Easing);
            slideAnimation.Commit(this, ANIM_SLIDE, length: Duration, easing: Easing);
        }
    }
}