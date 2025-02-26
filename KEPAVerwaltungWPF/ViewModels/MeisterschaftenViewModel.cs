using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Views;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MeisterschaftenViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public MeisterschaftenViewModel(DBService dbService)
    {
        _dbService = dbService;
        Titel = "Meisterschaften";
    }
    
    private async Task LoadAndSetDataAsync()
    {
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                Meisterschaften = await _dbService.GetMeisterschaftenAsync();
                Meisterschaftstypen = await _dbService.GetMeisterschaftstypenAsync();
                AktiveMitglieder = new ObservableCollection<AktiveSpieler>(await _dbService.GetAktiveMitgliederAsync());
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("MeisterschaftenViewModel", "LoadAndSetData", ex.ToString());
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    public void HandleDrop(object droppedData, string targetGridName)
    {
        if (droppedData is AktiveSpieler data)
        {
            switch (targetGridName)
            {
                case "dgAktiveMitglieder":
                    if (AktiveTeilnehmer.Contains(data))
                        AktiveTeilnehmer.Remove(data);
                    AktiveMitglieder.Add(data);

                    break;
                case "dgAktiveSpieler":
                    if (AktiveMitglieder.Contains(data))
                        AktiveMitglieder.Remove(data);
                    AktiveTeilnehmer.Add(data);
                    
                    break;
            }
        }
    }
    
    [ObservableProperty] private List<Meisterschaftsliste> meisterschaften = new();
    [ObservableProperty] private Meisterschaftsliste currentMeisterschaft = new();
    [ObservableProperty] private List<Meisterschaftstyp> meisterschaftstypen = new();
    [ObservableProperty] private Meisterschaftstyp currentMeisterschaftstyp = new();
    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveMitglieder = new();
    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveTeilnehmer = new();
    [ObservableProperty] private bool btnNeuEnabled = true;
    [ObservableProperty] private bool btnBearbeitenEnabled = false;
    [ObservableProperty] private bool btnSpeichernEnabled = false;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsFieldReadonly))] private bool areFieldsEditable = false;
    public bool IsFieldReadonly => !AreFieldsEditable;
    
    [RelayCommand]
    public void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            LoadAndSetDataAsync();
            IsViewModelLoaded = true;
        }
    }

    [RelayCommand]
    public void SelectMeisterschaft(Meisterschaftsliste meisterschaft)
    {
        CurrentMeisterschaft = meisterschaft;
        
        CurrentMeisterschaftstyp.ID = CurrentMeisterschaft.MeisterschaftstypID;
        CurrentMeisterschaftstyp.Value = CurrentMeisterschaft.Meisterschaftstyp;

        //AktiveTeilnehmer = _dbService.GetAktiveTeilnehmer(CurrentMeisterschaft.ID);
    }

    [RelayCommand]
    public void NeueMeisterschaft()
    {
        CurrentMeisterschaft = new();
        
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
    }
    
    [RelayCommand]
    public void MeisterschaftBearbeiten()
    {
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
    }
    
    [RelayCommand]
    public void MeisterschaftSpeichern()
    {
        AreFieldsEditable = false;
        BtnNeuEnabled = true;
        BtnBearbeitenEnabled = true;
        BtnSpeichernEnabled = false;
    }
}