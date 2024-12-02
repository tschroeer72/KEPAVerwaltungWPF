using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Views;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MitgliederViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public MitgliederViewModel(DBService dbService)
    {
        _dbService = dbService;
        Titel = "Mitglieder";
    }

    private void LoadAndSetData()
    {
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;
                
                foreach (var item in _dbService.GetMitgliederliste())
                {
                    TreeNode objNode = new(item.Anzeigename);
                    //objNode.Name = item.Anzeigename;
                    objNode.Mitglied = item;

                    TreeNode? objFind = MitgliederTree.FirstOrDefault(f => f.Name == item.Initial);
                    if (objFind == null)
                    {
                        TreeNode objFirst = new(item.Initial);
                        //objFirst.Name = item.Initial;
                        objFirst.Mitglied = null;
                        objFirst.Children.Add(objNode);
                        MitgliederTree.Add(objFirst);
                    }
                    else
                    {
                        objFind.Children.Add(objNode);
                    }
                }

                foreach (var item in MitgliederTree)
                {
                    string strNodeText = item.Name;
                    Int32 intCountChilds = item.Children.Count;
                    Int32 intPos = strNodeText.IndexOf(' ');
                    string strNodeNewText = string.Empty;
                    if (intPos == -1)
                        strNodeNewText += $" ({intCountChilds})";
                    else
                        strNodeNewText = strNodeText.Substring(0, intPos - 1) + $" ({intCountChilds})";
            
                    item.Name = strNodeNewText;
                }
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("MitgliederViewModel", "LoadAndSetData", ex.ToString());
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    [ObservableProperty] private ObservableCollection<TreeNode> mitgliederTree = new();
    [ObservableProperty] private MitgliedDetails currentMitglied = new();
    [ObservableProperty] private List<StatistikSpielerErgebnisse> statistikSpielerErgebnisse = new();
    [ObservableProperty] private List<StatistikSpieler> statistikSpieler = new();
    
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
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                if (selectedNode.Mitglied != null)
                {
                    CurrentMitglied = _dbService.GetMitgliedDetails(selectedNode.Mitglied.ID);
                    StatistikSpielerErgebnisse = _dbService.GetStatistikSpielerErgebnisse(selectedNode.Mitglied.ID);
                    StatistikSpieler = _dbService.GetStatistikSpieler(selectedNode.Mitglied.ID);
                }
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("MitgliederViewModel", "LoadAndSetData", ex.ToString());
        }
        finally
        {
            IsPageBusy = false;
        }
        
        // CurrentMitglied = new();
        // CurrentMitglied.ID = selectedNode.Mitglied.ID;
        // CurrentMitglied.Vorname = selectedNode.Mitglied.Vorname;
        // CurrentMitglied.Nachname = selectedNode.Mitglied.Nachname;
        // CurrentMitglied.Notizen = selectedNode.Mitglied.Vorname;
        // CurrentMitglied.Bemerkungen = selectedNode.Mitglied.Nachname;
        //
        // DelShowMainInfoFlyout?.Invoke($"{CurrentMitglied.Nachname} wurde ausgewählt");
    }
}