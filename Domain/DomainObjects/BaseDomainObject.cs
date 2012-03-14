using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using NaughtySpirit.SimsRunner.Domain.Attributes;
using Timer = System.Timers.Timer;
namespace NaughtySpirit.SimsRunner.Domain.DomainObjects
{
    public abstract class BaseDomainObject : IFormulable
    {
        private readonly Timer _clickTimer;
        private int _clickCount;
        private bool _isDragging;
        private IInputElement _relativeTarget;

        public delegate void MouseDoubleClickHandler(object sender);

        public delegate void MouseDragHandler(object sender, Point dragPoint);

        public delegate void MouseClickHandler(object sender);

        public event MouseDoubleClickHandler MouseDoubleClick;
        public event MouseDragHandler MouseDrag;
        public event MouseClickHandler MouseClick;

        protected BaseDomainObject()
        {
            _clickTimer = new Timer(150);
            _clickTimer.Elapsed += OnClickTimerElapsedHandler;
            SelectionEnabled = false;
        }

        public bool SelectionEnabled { get; set; }

        [Editable]
        public string Name { get; set; }

        public void OnEnableSelectionHandler()
        {
            SelectionEnabled = true;
        }

        public void OnDisableSelectionHandler()
        {
            SelectionEnabled = false;
        }

        public void OnMouseClick(object sender)
        {
            var handler = MouseClick;
            if (handler != null) handler(sender);
        }

        private void OnMouseDoubleClick()
        {
            var handler = MouseDoubleClick;
            if (handler != null) handler(this);
        }

        private void OnDrag(object sender, Point dragPoint)
        {
            var handler = MouseDrag;
            if (handler != null && SelectionEnabled) handler(sender, dragPoint);
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
            OnMouseClick(this);
        }

        private void OnMouseMoveHandler(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_isDragging)
            {
                var dragPoint = mouseEventArgs.GetPosition(_relativeTarget);
                OnDrag(sender, dragPoint);
            }
        }

        private void OnMouseDownHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _clickTimer.Stop();
            _clickCount++;
            _clickTimer.Start();
        }

        public string GetFormula()
        {
            return DoGetFormula().Replace(" ", "");
        }

        protected abstract string DoGetFormula();
    }
}