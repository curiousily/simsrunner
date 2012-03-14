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

        private Rectangle _rect;

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
            _rect = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.MidnightBlue
            };
            MakeDraggable(_rect, relativeTo: canvas);
            Drag += OnDrag;
            canvas.Children.Add(_rect.FromMidPoint(_midPoint, Width, Height));
        }

        private void OnDrag(Point dragPoint)
        {
            Canvas.SetLeft(_rect, dragPoint.X - 25);
            Canvas.SetTop(_rect, dragPoint.Y - 25);
        }
    }
}