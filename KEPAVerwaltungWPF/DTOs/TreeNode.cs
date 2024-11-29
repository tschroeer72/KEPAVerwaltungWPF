using System.Collections.ObjectModel;

namespace KEPAVerwaltungWPF.DTOs;

public class TreeNode
{
    public string Name { get; set; }
    
    public Mitgliederliste? Mitglied { get; set; }
    
    public ObservableCollection<TreeNode> Children { get; set; }
    
    public bool IsExpanded { get; set; }
    
    public bool IsSelected { get; set; }

    public TreeNode(string name, Mitgliederliste? mitglied = null)
    {
        Name = name;
        Mitglied = mitglied ?? null;
        Children = new ObservableCollection<TreeNode>();
    }
}