using Android.Content;
using AndroidX.RecyclerView.Widget;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CarouselView), typeof(BigCart.Droid.Renderers.CarouselViewRenderer))]
namespace BigCart.Droid.Renderers
{
    class CarouselViewRenderer : Xamarin.Forms.Platform.Android.CarouselViewRenderer
    {
        public CarouselViewRenderer(Context context) : base(context)
        {
        }

        protected override LayoutManager SelectLayoutManager(IItemsLayout layoutSpecification)
        {
            if (layoutSpecification is LinearItemsLayout linearItemsLayout)
            {
                int orientation = linearItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal
                        ? LinearLayoutManager.Horizontal
                        : LinearLayoutManager.Vertical;

                return new LinearLayoutManagerEx(this, orientation);
            }

            return base.SelectLayoutManager(layoutSpecification);
        }

        private class LinearLayoutManagerEx : LinearLayoutManager
        {
            private RecyclerView _recyclerView;

            public LinearLayoutManagerEx(RecyclerView recyclerView, int orientation) : base(recyclerView.Context, orientation, false)
            {
                _recyclerView = recyclerView;
            }

            protected override void CalculateExtraLayoutSpace(State state, int[] extraLayoutSpace)
            {
                base.CalculateExtraLayoutSpace(state, extraLayoutSpace);

                if (Orientation == Horizontal)
                    extraLayoutSpace[1] = _recyclerView.Width;
            }
        }
    }
}