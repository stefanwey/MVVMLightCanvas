using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace CanvasGui.Helpers
{
    public class MouseEventArgsToPointConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var args = (MouseEventArgs)value;
            var element = (FrameworkElement)parameter;

            var point = args.GetPosition(element);
            return point;
        }
    }
}