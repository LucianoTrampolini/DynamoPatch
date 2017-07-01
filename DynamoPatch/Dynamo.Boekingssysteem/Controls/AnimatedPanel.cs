using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dynamo.Boekingssysteem.Controls
{
    public class AnimatedPanel : StackPanel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children == null
                || Children.Count == 0)
                return finalSize;

            TranslateTransform trans = null;
            double curX = 0,
                curY = 0,
                curLineHeight = 0;

            foreach (UIElement child in Children)
            {
                trans = child.RenderTransform as TranslateTransform;
                if (trans == null)
                {
                    child.RenderTransformOrigin = new Point(0, 0);
                    trans = new TranslateTransform();
                    child.RenderTransform = trans;
                }

                if (curX + child.DesiredSize.Width > finalSize.Width)
                {
                    //Wrap to next line
                    curY += curLineHeight;
                    curX = 0;
                    curLineHeight = 0;
                }

                child.Arrange(new Rect(0, 0, child.DesiredSize.Width, child.DesiredSize.Height));

                trans.BeginAnimation(
                    TranslateTransform.XProperty,
                    new DoubleAnimation(curX, TimeSpan.FromMilliseconds(200)),
                    HandoffBehavior.Compose);

                trans.BeginAnimation(
                    TranslateTransform.YProperty,
                    new DoubleAnimation(curY, TimeSpan.FromMilliseconds(200)),
                    HandoffBehavior.Compose);

                curX += child.DesiredSize.Width;
                if (child.DesiredSize.Height > curLineHeight)
                    curLineHeight = child.DesiredSize.Height;
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            double curX = 0,
                curY = 0,
                curLineHeight = 0;
            foreach (UIElement child in Children)
            {
                child.Measure(infiniteSize);

                if (curX + child.DesiredSize.Width > availableSize.Width)
                {
                    //Wrap to next line
                    curY += curLineHeight;
                    curX = 0;
                    curLineHeight = 0;
                }

                curX += child.DesiredSize.Width;
                if (child.DesiredSize.Height > curLineHeight)
                    curLineHeight = child.DesiredSize.Height;
            }

            curY += curLineHeight;

            Size resultSize = new Size();

            resultSize.Width = double.IsPositiveInfinity(availableSize.Width)
                ? curX
                : availableSize.Width;

            resultSize.Height = double.IsPositiveInfinity(availableSize.Height)
                ? curY
                : availableSize.Height;

            return resultSize;
        }
    }
}