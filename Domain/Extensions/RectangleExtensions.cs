using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NaughtySpirit.SimsRunner.Domain.Extensions
{
    public static class RectangleExtensions
    {
         public static Rectangle FromMidPoint(this Rectangle rectangle, Point midPoint, int width, int height)
         {             
             var startPoint = new Point(midPoint.X - width / 2, midPoint.Y - height / 2);
             Canvas.SetLeft(rectangle, startPoint.X);
             Canvas.SetTop(rectangle, startPoint.Y);
             rectangle.Width = width;
             rectangle.Height = height;
             return rectangle;
         }
    }
}