using System.Windows;
using System.Windows.Media;

namespace TimeManager.Presentation.Views.Extension
{
    public static class ControlExtensions
    {
        public static T GetVisualParent<T>(this DependencyObject control) where T : Visual
        {
            while ((control != null) && !(control is T))
            {
                control = VisualTreeHelper.GetParent(control);
            }
            return control as T;
        }
    }
}
