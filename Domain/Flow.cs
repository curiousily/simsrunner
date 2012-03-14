using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace NaughtySpirit.SimsRunner.Domain
{
    public class Flow : IView
    {
        private const int Width = 20;
        private const int Height = 20;
        private const int MouseOffset = 3;

        private readonly Stock _sourceStock;
        private readonly Stock _targetStock;
        private bool _isDragging;
        private Canvas _canvas;
        private Line _targetLine;
        private Line _sourceLine;

        public Flow(Stock sourceStock, Stock targetStock)
        {
            _sourceStock = sourceStock;
            _sourceStock.Drag += OnSourceStockDragHandler;
            _targetStock = targetStock;
            _targetStock.Drag += OnTargetStockDragHandler;
        }

        private void OnTargetStockDragHandler(Point dragPoint)
        {
            _targetLine.X2 = dragPoint.X + MouseOffset;
            _targetLine.Y2 = dragPoint.Y + MouseOffset;
        }

        private void OnSourceStockDragHandler(Point dragPoint)
        {
            _sourceLine.X1 = dragPoint.X + MouseOffset;
            _sourceLine.Y1 = dragPoint.Y + MouseOffset;
        }

        public void AddToCanvas(Canvas canvas)
        {
            _canvas = canvas;
            var flowMidPoint = new Point().AsMidPointOf(_sourceStock.MidPoint, _targetStock.MidPoint);
            _sourceLine = new Line().FromPoints(_sourceStock.MidPoint, flowMidPoint, Brushes.LawnGreen);
            _targetLine = new Line().FromPoints(flowMidPoint, _targetStock.MidPoint, Brushes.LawnGreen);            
            
            var flowRectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.PaleTurquoise
            };
            flowRectangle.MouseDown += RectOnMouseDownHandler;
            flowRectangle.MouseMove += RectOnMouseMoveHandler;
            flowRectangle.MouseUp += RectOnMouseUpHandler;
            canvas.Children.Add(_sourceLine);
            canvas.Children.Add(_targetLine);
            canvas.Children.Add(flowRectangle.FromMidPoint(flowMidPoint, Width, Height));
        }

        private void RectOnMouseUpHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.LeftButton == MouseButtonState.Released)
            {
                _isDragging = false;
            }
        }

        private void RectOnMouseMoveHandler(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_isDragging)
            {
                var rect = (Rectangle) sender;
                var dragPoint = mouseEventArgs.GetPosition(_canvas);
                dragPoint.Offset(MouseOffset, MouseOffset);
                _sourceLine.X2 = dragPoint.X;
                _sourceLine.Y2 = dragPoint.Y;

                _targetLine.X1 = dragPoint.X;
                _targetLine.Y1 = dragPoint.Y;
                Canvas.SetLeft(rect, dragPoint.X - Width / 2);
                Canvas.SetTop(rect, dragPoint.Y - Height / 2);
            }
        }

        private void RectOnMouseDownHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                _isDragging = true;
            }
        }
    }
}