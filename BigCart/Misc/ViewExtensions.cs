using Xamarin.Forms;

namespace BigCart
{
    public static class ViewExtensions
    {
        /// <summary>
        /// Gets the position of a view relative to the specified layout or the screen if no layout is specified.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="parentLayout">
        /// Parent layout that the position should be measured relative to. If this is null or 
        /// not a parent of the specified view, the absolute position of the screen will be returned instead.
        /// </param>
        /// <returns></returns>
        public static Point GetRelativePosition(this View view, Layout parentLayout = null)
        {
            double x = view.Width * 0.5f;
            double y = view.Height * 0.5f;

            View parent = view;
            do
            {
                x += parent.X;
                y += parent.Y;
                parent = parent.Parent as View;

            } while (parent != null && parent != parentLayout);

            return new Point(x, y);
        }
    }
}
