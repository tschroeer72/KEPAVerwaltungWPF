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

    private async Task LoadAndSetData()
    {
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;
                
                MitgliederTree.Clear();
                foreach (var item in await _dbService.GetMitgliederlisteAsync())
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
                /*
                foreach (var item in MitgliederTree)
                {
                    string strNodeText = item.Name;
                    Int32 intCountChilds = item.Children.Count;
                    Int32 intPos = strNodeText.IndexOf(' ');
                    string strNodeNewText = string.Empty;
                    if (intPos == -1)
                        strNodeNewText = strNodeText + $" ({intCountChilds})";
                    else
                        strNodeNewText = strNodeText.Substring(0, intPos - 1) + $" ({intCountChilds})";
            
                    item.Name = strNodeNewText;
                }
                */
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
    [ObservableProperty] private bool btnNeuEnabled = true;
    [ObservableProperty] private bool btnBearbeitenEnabled = false;
    [ObservableProperty] private bool btnSpeichernEnabled = false;
    [ObservableProperty] private bool btnDruckenEnabled = false;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsFieldReadonly))] private bool areFieldsEditable = false;
    public bool IsFieldReadonly => !AreFieldsEditable;
    
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
    public async void TvSelectedItemChanged(TreeNode selectedNode)
    {
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                if (selectedNode.Mitglied != null)
                {
                    CurrentMitglied = await _dbService.GetMitgliedDetailsAsync(selectedNode.Mitglied.ID);
                    StatistikSpielerErgebnisse = await _dbService.GetStatistikSpielerErgebnisseAsync(selectedNode.Mitglied.ID);
                    StatistikSpieler = await _dbService.GetStatistikSpielerAsync(selectedNode.Mitglied.ID);
                    
                    BtnNeuEnabled = true;
                    BtnBearbeitenEnabled = true;
                    BtnSpeichernEnabled = false;
                    BtnDruckenEnabled = true;
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

    [RelayCommand]
    public void NeuesMitglied()
    {
        CurrentMitglied = new();
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
        BtnDruckenEnabled = false;
    }

    [RelayCommand]
    public void MitgliedBearbeiten()
    {
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
        BtnDruckenEnabled = false;
    }
    
    [RelayCommand]
    public async Task MitgliedSpeichern()
    {
        if (ViewManager.ShowConfirmationWindow("Wollen Sie die geänderten Daten speichern?"))
        {
            await _dbService.SaveMitgliedAsync(CurrentMitglied);
            await LoadAndSetData();
            
            CurrentMitglied = new();
            
            AreFieldsEditable = false;
            BtnNeuEnabled = true;
            BtnBearbeitenEnabled = true;
            BtnSpeichernEnabled = false;
            BtnDruckenEnabled = false;
        }
    }
}