using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NaughtySpirit.SimsRunner.Domain.Extensions
{
    public static class LineExtensions
    {
         public static Line FromPoints(this Line line, Point p1, Point p2, Brush brush)
         {
            return new Line
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                Stroke = brush
            };
         }
    }
}