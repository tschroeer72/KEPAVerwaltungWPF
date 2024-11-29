using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MitgliederViewModel : BaseViewModel
{
    public MitgliederViewModel()
    {
        Titel = "Mitglieder";
    }

    private void LoadAndSetData()
    {
        Mitgliederliste objMitglied1 = new();
        objMitglied1.ID = 1;
        objMitglied1.Vorname = "Karl-Heinz";
        objMitglied1.Nachname = "Bohn";
        
        Mitgliederliste objMitglied2 = new();
        objMitglied2.ID = 2;
        objMitglied2.Vorname = "Thorsten";
        objMitglied2.Nachname = "Schröer";
        
        Mitgliederliste objMitglied3 = new();
        objMitglied3.ID = 3;
        objMitglied3.Vorname = "Wolfgang";
        objMitglied3.Nachname = "Schmidt";
        
        MitgliederTree = new ObservableCollection<TreeNode>
        {
            new TreeNode("B (1)")
            {
                Children = new ObservableCollection<TreeNode>
                {
                    new TreeNode("Bohn, Karl-Heinz", objMitglied1),
                },
                IsExpanded = true
            },
            new TreeNode("S (2)")
            {
                Children = new ObservableCollection<TreeNode>
                {
                    new TreeNode("Schröer, Thorsten", objMitglied2),
                    new TreeNode("Schmidt, Wolfgang", objMitglied3),
                },
                IsExpanded = true
            }
        };
    }

    [ObservableProperty] 
    ObservableCollection<TreeNode> mitgliederTree;
    
    [ObservableProperty] 
    MitgliedDetails currentMitglied = new();
    
    [RelayCommand]
    public void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            LoadAndSetData();
            IsViewModelLoaded = true;
        }
    }

    [RelayCommand]
    public void TvSelectedItemChanged(TreeNode selectedNode)
    {
        CurrentMitglied = new();
        CurrentMitglied.ID = selectedNode.Mitglied.ID;
        CurrentMitglied.Vorname = selectedNode.Mitglied.Vorname;
        CurrentMitglied.Nachname = selectedNode.Mitglied.Nachname;
        CurrentMitglied.Notizen = selectedNode.Mitglied.Vorname;
        CurrentMitglied.Bemerkungen = selectedNode.Mitglied.Nachname;
        
        DelShowMainInfoFlyout?.Invoke($"{CurrentMitglied.Nachname} wurde ausgewählt");
    }
}