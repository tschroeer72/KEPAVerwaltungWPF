using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace KEPAVerwaltungWPF.Helper;

public class SelectedItemsBehavior : Behavior<ListBox>
{
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(SelectedItemsBehavior), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemsChanged));

    public IList SelectedItems
    {
        get => (IList)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.SelectionChanged += OnSelectionChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.SelectionChanged -= OnSelectionChanged;
    }

    private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SelectedItemsBehavior behavior && behavior.AssociatedObject != null)
        {
            behavior.UpdateSelectedItems();
        }
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItems == null) return;

        foreach (var item in e.RemovedItems) SelectedItems.Remove(item);
        foreach (var item in e.AddedItems) SelectedItems.Add(item);
    }

    private void UpdateSelectedItems()
    {
        if (AssociatedObject.SelectedItems is IList selectedItems && SelectedItems != null)
        {
            selectedItems.Clear();
            foreach (var item in SelectedItems) selectedItems.Add(item);
        }
    }
}