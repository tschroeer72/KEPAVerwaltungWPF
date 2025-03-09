using System.Windows;
using System.Windows.Media;

namespace KEPAVerwaltungWPF.Helper;

public static class VisualTreeHelperExtensions
{
    public static T GetParentOfType<T>(this DependencyObject element) where T : DependencyObject
    {
        DependencyObject parent = element;
        while (parent != null && !(parent is T))
        {
            parent = VisualTreeHelper.GetParent(parent);
        }
        return parent as T;
    }
}