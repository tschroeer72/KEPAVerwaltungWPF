using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace KEPAVerwaltungWPF.Helper;

public static class ListBoxHelper
{
    public static readonly DependencyProperty SynchronizedSelectedItemsProperty =
        DependencyProperty.RegisterAttached(
            "SynchronizedSelectedItems",
            typeof(IList),
            typeof(ListBoxHelper),
            new PropertyMetadata(null, OnSynchronizedSelectedItemsChanged));

    public static void SetSynchronizedSelectedItems(DependencyObject element, IList value)
    {
        element.SetValue(SynchronizedSelectedItemsProperty, value);
    }

    public static IList GetSynchronizedSelectedItems(DependencyObject element)
    {
        return (IList)element.GetValue(SynchronizedSelectedItemsProperty);
    }

    private static void OnSynchronizedSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListBox listBox)
        {
            if (e.OldValue is IList oldList)
            {
                listBox.SelectionChanged -= ListBox_SelectionChanged;
            }

            if (e.NewValue is IList newList)
            {
                listBox.SelectionChanged += ListBox_SelectionChanged;
            }
        }
    }

    private static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox listBox)
        {
            IList selectedItems = GetSynchronizedSelectedItems(listBox);
            if (selectedItems == null) return;

            selectedItems.Clear();
            foreach (var item in listBox.SelectedItems)
            {
                selectedItems.Add(item);
            }
        }
    }
}