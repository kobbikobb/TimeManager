using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace TimeManagerLib.ViewModel.Extension
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
