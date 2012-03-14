using System.Windows;
using System.Windows.Controls;

namespace NaughtySpirit.SimsRunner.Domain
{
    public interface IView
    {
        void AddToCanvas(Canvas canvas);
    }
}