using System.Windows;

namespace NaughtySpirit.SimsRunner.Domain.Extensions
{
    public static class PointExtensions
    {
        public static Point AsMidPointOf(this Point point, Point p1, Point p2)
        {
            point.X = (p1.X + p2.X) / 2;
            point.Y = (p1.Y + p2.Y) / 2;
            return point;
        }
    }
}