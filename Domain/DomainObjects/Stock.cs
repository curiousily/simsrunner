using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using NaughtySpirit.SimsRunner.Domain.Attributes;
using NaughtySpirit.SimsRunner.Domain.Extensions;

namespace NaughtySpirit.SimsRunner.Domain.DomainObjects
{
    public class Stock : BaseDomainObject, IView
    {
        private const int Width = 50;
        private const int Height = 50;

        private readonly Point _midPoint;

        public Stock(Point midPoint)
        {
            _midPoint = midPoint;
        }

        [Editable]
        public string InitialValue { get; set; } 

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
            var rect = (Rectangle) sender;
            Canvas.SetLeft(rect, dragPoint.X - 25);
            Canvas.SetTop(rect, dragPoint.Y - 25);
        }
    }
}