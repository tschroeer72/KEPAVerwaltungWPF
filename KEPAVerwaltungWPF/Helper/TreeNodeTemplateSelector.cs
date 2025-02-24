using System.Windows;
using System.Windows.Controls;
using KEPAVerwaltungWPF.DTOs;

namespace KEPAVerwaltungWPF.Helper;

public class TreeNodeTemplateSelector : DataTemplateSelector
{
    public DataTemplate ParentTemplate { get; set; }
    public DataTemplate ChildTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        var treeNode = item as TreeNode;
        if (treeNode != null && treeNode.Children != null && treeNode.Children.Count > 0)
        {
            return ParentTemplate;
        }
        return ChildTemplate;
    }
}