using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Validations;
using KEPAVerwaltungWPF.Views;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Quality;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MitgliederViewModel : BaseViewModel
{
    private readonly IMapper _mapper;
    private readonly DBService _dbService;
    private readonly MitgliedAnlegenValidation _mitgliedAnlegenValidation;
    private readonly MitgliedAktualisierenValidation _mitgliedAktualisierenValidation;

    public MitgliederViewModel(IMapper mapper, DBService dbService, MitgliedAnlegenValidation mitgliedAnlegenValidation, MitgliedAktualisierenValidation mitgliedAktualisierenValidation)
    {
        _mapper = mapper;
        _dbService = dbService;
        _mitgliedAnlegenValidation = mitgliedAnlegenValidation;
        _mitgliedAktualisierenValidation = mitgliedAktualisierenValidation;
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
    [ObservableProperty] private string validationMessage = string.Empty;
    [ObservableProperty] private bool btnNeuEnabled = true;
    [ObservableProperty] private bool btnAbbrechenEnabled = true;
    [ObservableProperty] private bool btnBearbeitenEnabled = false;
    [ObservableProperty] private bool btnSpeichernEnabled = false;
    [ObservableProperty] private bool btnDruckenEnabled = false;
    
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsFieldReadonly))] private bool areFieldsEditable = false;
    public bool IsFieldReadonly => !AreFieldsEditable;
    
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(BtnAbbrechenVisibility))] private bool btnNeuVisibility = true;
    public bool BtnAbbrechenVisibility => !BtnNeuVisibility;
    
    [ObservableProperty] private bool druckErgebnisse = true;
    [ObservableProperty] private bool druckStatistik = true;
    
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
        BtnNeuVisibility = false;
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
        BtnDruckenEnabled = false;
    }

    [RelayCommand]
    public void Abbrechen()
    {
        if (ViewManager.ShowConfirmationWindow("Wollen Sie die Bearbeitung wirklich abbrechen?"))
        {
            //CurrentMitglied = new();
            AreFieldsEditable = false;
            BtnNeuEnabled = true;
            BtnNeuVisibility = true;
            BtnBearbeitenEnabled = false;
            BtnSpeichernEnabled = false;
            BtnDruckenEnabled = false;
        }
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
    public async Task MitgliedSpeichernAsync()
    {
        ValidationMessage = "";

        if (ViewManager.ShowConfirmationWindow("Wollen Sie die geänderten Daten speichern?"))
        {
            if (CurrentMitglied.ID == -1)
            {
                var validationResult = await _mitgliedAnlegenValidation.ValidateAsync(CurrentMitglied);
                if (!validationResult.IsValid)
                {
                    ValidationMessage = validationResult.ToString();
                    return;
                }
            }
            else
            {
                var validationResult = await _mitgliedAktualisierenValidation.ValidateAsync(CurrentMitglied);
                if (!validationResult.IsValid)
                {
                    ValidationMessage = validationResult.ToString();
                    return;
                }
            }

            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                await _dbService.SaveMitgliedAsync(CurrentMitglied);
                await LoadAndSetData();

                if (CurrentMitglied.ID == -1)
                    DelShowMainInfoFlyout.Invoke(
                        $"{CurrentMitglied.Vorname} {currentMitglied.Nachname} wurde angelegt");
                else
                    DelShowMainInfoFlyout.Invoke(
                        $"{CurrentMitglied.Vorname} {currentMitglied.Nachname} wurde aktualisiert");
                
                IsPageBusy = false;
            }

            CurrentMitglied = new();
            
            AreFieldsEditable = false;
            BtnNeuEnabled = true;
            BtnBearbeitenEnabled = true;
            BtnSpeichernEnabled = false;
            BtnDruckenEnabled = false;
        }
    }

    // public VpeControl DruckSpielerErgebnisseStatistik()
    // {
    //     
    // }
}