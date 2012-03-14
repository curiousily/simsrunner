using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Timer = System.Timers.Timer;
namespace NaughtySpirit.SimsRunner.Domain
{
    public abstract class BaseDomainObject
    {
        private readonly Timer _clickTimer;
        private int _clickCount;
        private bool _isDragging;
        private IInputElement _relativeTarget;

        public delegate void MouseDoubleClickHandler(BaseDomainObject sender);

        public delegate void DragHandler(Point dragPoint);

        public event MouseDoubleClickHandler MouseDoubleClick;
        public event DragHandler Drag;

        protected BaseDomainObject()
        {
            _clickTimer = new Timer(200);
            _clickTimer.Elapsed += OnClickTimerElapsedHandler;
        }

        private void OnMouseDoubleClick()
        {
            var handler = MouseDoubleClick;
            if (handler != null) handler(this);
        }

        private void OnDrag(Point dragPoint)
        {
            var handler = Drag;
            if (handler != null) handler(dragPoint);
        }

        private void OnClickTimerElapsedHandler(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _clickTimer.Stop();
            if (_clickCount == 1)
            {
                _isDragging = true;
            }
            else
            {
                if (MouseDoubleClick != null)
                {
                    RunOnUiThread(OnMouseDoubleClick);
                }
            }
            _clickCount = 0;
        }

        private static void RunOnUiThread(ThreadStart method)
        {
            var thread = new Thread(method);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();                 
        }

        protected void MakeDraggable(UIElement element, IInputElement relativeTo)
        {
            _relativeTarget = relativeTo;
            element.MouseDown += OnMouseDownHandler;
            element.MouseMove += OnMouseMoveHandler;
            element.MouseUp += OnMouseUpHandler;
        }

        private void OnMouseUpHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _isDragging = false;
        }

        private void OnMouseMoveHandler(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_isDragging)
            {
                var dragPoint = mouseEventArgs.GetPosition(_relativeTarget);
                OnDrag(dragPoint);
            }
        }

        private void OnMouseDownHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _clickTimer.Stop();
            _clickCount++;
            _clickTimer.Start();
        }
    }
}