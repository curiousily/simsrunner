using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace NaughtySpirit.SimsRunner.Domain
{
    public class Stock : BaseDomainObject, IView
    {
        private const int Width = 50;
        private const int Height = 50;

        private readonly Point _midPoint;

//        private Rectangle _rect;

        public Stock(Point midPoint)
        {
            _midPoint = midPoint;
        }

        public Point MidPoint
        {
            get { return _midPoint; }
        }

        public void AddToCanvas(Canvas canvas)
        {
            var rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.MidnightBlue
            };
            MakeDraggable(rectangle, relativeTo: canvas);
            MouseDrag += OnMouseDrag;
            canvas.Children.Add(rectangle.FromMidPoint(_midPoint, Width, Height));
        }

        private void OnMouseDrag(object sender, Point dragPoint)
        {
            var rect = sender as Rectangle;
            Canvas.SetLeft(rect, dragPoint.X - 25);
            Canvas.SetTop(rect, dragPoint.Y - 25);
        }
    }
}