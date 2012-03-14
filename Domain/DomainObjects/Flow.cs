using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using NaughtySpirit.SimsRunner.Domain.Attributes;
using NaughtySpirit.SimsRunner.Domain.Extensions;

namespace NaughtySpirit.SimsRunner.Domain.DomainObjects
{
    public class Flow : BaseDomainObject, IView
    {
        private const int Width = 20;
        private const int Height = 20;
        private const int MouseOffset = 3;

        private readonly Stock _sourceStock;
        private readonly Stock _targetStock;
        private Line _targetLine;
        private Line _sourceLine;

        public Flow(Stock sourceStock, Stock targetStock)
        {
            _sourceStock = sourceStock;
            _sourceStock.OutFormulable = this;
            _sourceStock.MouseDrag += OnSourceStockMouseDragHandler;
            _targetStock = targetStock;
            _targetStock.InFormulable = this;
            _targetStock.MouseDrag += OnTargetStockMouseDragHandler;
        }

        [Editable]
        public string Formula { get; set; }

        private void OnTargetStockMouseDragHandler(object sender, Point dragPoint)
        {
            dragPoint.Offset(-MouseOffset, -MouseOffset);
            _targetLine.X2 = dragPoint.X;
            _targetLine.Y2 = dragPoint.Y;
        }

        private void OnSourceStockMouseDragHandler(object sender, Point dragPoint)
        {
            dragPoint.Offset(MouseOffset, MouseOffset);
            _sourceLine.X1 = dragPoint.X;
            _sourceLine.Y1 = dragPoint.Y;
        }

        public void AddToCanvas(Canvas canvas)
        {
            var flowMidPoint = new Point().AsMidPointOf(_sourceStock.MidPoint, _targetStock.MidPoint);
            _sourceLine = new Line().FromPoints(_sourceStock.MidPoint, flowMidPoint, Brushes.LawnGreen);
            _targetLine = new Line().FromPoints(flowMidPoint, _targetStock.MidPoint, Brushes.LawnGreen);            
            
            var flowRectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.PaleTurquoise
            };
            MakeDraggable(flowRectangle, relativeTo: canvas);
            MouseDrag += OnMouseDragHandler;
            canvas.Children.Add(_sourceLine);
            canvas.Children.Add(_targetLine);
            canvas.Children.Add(flowRectangle.FromMidPoint(flowMidPoint, Width, Height));
        }

        private void OnMouseDragHandler(object sender, Point dragPoint)
        {
            var rect = (Rectangle) sender;
            dragPoint.Offset(MouseOffset, MouseOffset);
            _sourceLine.X2 = dragPoint.X;
            _sourceLine.Y2 = dragPoint.Y;

            _targetLine.X1 = dragPoint.X;
            _targetLine.Y1 = dragPoint.Y;
            Canvas.SetLeft(rect, dragPoint.X - Width / 2);
            Canvas.SetTop(rect, dragPoint.Y - Height / 2);
        }

        protected override string DoGetFormula()
        {
            return Formula;
        }
    }
}