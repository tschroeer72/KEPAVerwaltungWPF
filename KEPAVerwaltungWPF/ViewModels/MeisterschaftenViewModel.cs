using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Validations;
using KEPAVerwaltungWPF.Views;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MeisterschaftenViewModel : BaseViewModel
{
    private readonly IMapper _mapper;
    private readonly DBService _dbService;
    private readonly MeisterschaftValidation _meisterschaftValidation;
    //private readonly MeisterschaftAktualisierenValidation _meisterschaftAktualisierenValidation;

    public MeisterschaftenViewModel(IMapper mapper, DBService dbService,
        MeisterschaftValidation meisterschaftValidation) //, MeisterschaftAktualisierenValidation meisterschaftAktualisierenValidation
    {
        _mapper = mapper;
        _dbService = dbService;
        _meisterschaftValidation = meisterschaftValidation;
        //_meisterschaftAktualisierenValidation = meisterschaftAktualisierenValidation;

        Titel = "Meisterschaften";
    }

    private async Task LoadAndSetDataAsync()
    {
        try
        {
            if (IsPageNotBusy)
            {
                IsPageBusy = true;

                CurrentMeisterschaft = new();
                CurrentMeisterschaftstyp = new();

                Meisterschaften = await _dbService.GetMeisterschaftenAsync();
                Meisterschaftstypen = await _dbService.GetMeisterschaftstypenAsync();
                AktiveMitglieder = new ObservableCollection<AktiveSpieler>(await _dbService.GetAktiveMitgliederAsync());
            }
        }
        catch (Exception ex)
        {
            ViewManager.ShowErrorWindow("MeisterschaftenViewModel", "LoadAndSetDataAsync", ex.ToString());
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    public async Task HandleDrop(object droppedData, string targetGridName)
    {
        if (droppedData is AktiveSpieler data)
        {
            switch (targetGridName)
            {
                case "dgAktiveMitglieder":
                    if (AktiveTeilnehmer.Contains(data))
                        AktiveTeilnehmer.Remove(data);
                    AktiveMitglieder.Add(data);
                    await _dbService.DeleteTeilnehmerAsync(CurrentMeisterschaft.ID, data.ID);
                    
                    break;
                case "dgAktiveSpieler":
                    if (AktiveMitglieder.Contains(data))
                        AktiveMitglieder.Remove(data);
                    AktiveTeilnehmer.Add(data);
                    await _dbService.SaveTeilnehmerAsync(CurrentMeisterschaft.ID, data.ID);
                    
                    break;
            }
            
            AktiveTeilnehmer =
                new ObservableCollection<AktiveSpieler>(
                    await _dbService.GetAktiveTeilnehmerAsync(CurrentMeisterschaft.ID));
            AktiveMitglieder =
                new ObservableCollection<AktiveSpieler>(
                    await _dbService.GetAktiveMitgliederAsync(CurrentMeisterschaft.ID));
        }
    }

    [ObservableProperty] private List<Meisterschaftsdaten> meisterschaften = new();
    [ObservableProperty] private Meisterschaftsdaten currentMeisterschaft = new();
    [ObservableProperty] private List<Meisterschaftstyp> meisterschaftstypen = new();
    [ObservableProperty] private Meisterschaftstyp currentMeisterschaftstyp = new();
    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveMitglieder = new();
    [ObservableProperty] private ObservableCollection<AktiveSpieler> aktiveTeilnehmer = new();
    [ObservableProperty] private string validationMessage = string.Empty;
    [ObservableProperty] private bool btnNeuEnabled = true;
    [ObservableProperty] private bool btnAbbrechenEnabled = true;
    [ObservableProperty] private bool btnBearbeitenEnabled = false;
    [ObservableProperty] private bool btnSpeichernEnabled = false;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsFieldReadonly))]
    private bool areFieldsEditable = false;

    public bool IsFieldReadonly => !AreFieldsEditable;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(BtnAbbrechenVisibility))]
    private bool btnNeuVisibility = true;

    public bool BtnAbbrechenVisibility => !BtnNeuVisibility;

    [RelayCommand]
    public async Task GetInitialDataAsync()
    {
        if (!IsViewModelLoaded)
        {
            await LoadAndSetDataAsync();
            IsViewModelLoaded = true;
        }
    }

    [RelayCommand]
    public async Task SelectMeisterschaft(Meisterschaftsdaten? selectedMeisterschaft)
    {
        if (selectedMeisterschaft != null)
        {
            //CurrentMeisterschaft = selectedMeisterschaft;
            CurrentMeisterschaft = _mapper.Map<Meisterschaftsdaten>(selectedMeisterschaft);

            CurrentMeisterschaftstyp.ID = CurrentMeisterschaft.MeisterschaftstypID;
            CurrentMeisterschaftstyp.Value = CurrentMeisterschaft.Meisterschaftstyp;

            AktiveTeilnehmer =
                new ObservableCollection<AktiveSpieler>(
                    await _dbService.GetAktiveTeilnehmerAsync(CurrentMeisterschaft.ID));
            AktiveMitglieder =
                new ObservableCollection<AktiveSpieler>(
                    await _dbService.GetAktiveMitgliederAsync(CurrentMeisterschaft.ID));
            
            BtnNeuVisibility = true;
            AreFieldsEditable = false;
            BtnNeuEnabled = true;
            BtnAbbrechenEnabled = true;
            BtnBearbeitenEnabled = true;
            BtnSpeichernEnabled = false;
        }
    }

    [RelayCommand]
    public void MeisterschaftstypChanged(Meisterschaftstyp selectedMeisterschaftstyp)
    {
        if (CurrentMeisterschaft != null && CurrentMeisterschaftstyp != null)
        {
            CurrentMeisterschaft.MeisterschaftstypID = selectedMeisterschaftstyp.ID;
            CurrentMeisterschaft.Meisterschaftstyp = selectedMeisterschaftstyp.Value;
        }
    }

    [RelayCommand]
    public void NeueMeisterschaft()
    {
        CurrentMeisterschaft = new();

        BtnNeuVisibility = false;
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnAbbrechenEnabled = true;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
    }

    [RelayCommand]
    public void Abbrechen()
    {
        BtnNeuVisibility = true;
        AreFieldsEditable = true;
        BtnNeuEnabled = true;
        BtnAbbrechenEnabled = false;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = false;
    }

    [RelayCommand]
    public void MeisterschaftBearbeiten()
    {
        AreFieldsEditable = true;
        BtnNeuEnabled = false;
        BtnNeuVisibility = false;
        BtnAbbrechenEnabled = true;
        BtnBearbeitenEnabled = false;
        BtnSpeichernEnabled = true;
    }

    [RelayCommand]
    public async Task MeisterschaftSpeichernAsync()
    {
        ValidationMessage = "";

        if (ViewManager.ShowConfirmationWindow("Wollen Sie die geänderten Daten speichern?"))
        {
            // if(CurrentMeisterschaft.ID == -1)
            // {
            //     var validationResult = await _meisterschaftValidation.ValidateAsync(CurrentMeisterschaft);
            //     if (!validationResult.IsValid)
            //     {
            //         ValidationMessage = validationResult.ToString();
            //         return;
            //     }
            // }
            // else
            // {
            //     var validationResult = await _meisterschaftAktualisierenValidation.ValidateAsync(CurrentMeisterschaft);
            //     if (!validationResult.IsValid)
            //     {
            //         ValidationMessage = validationResult.ToString();
            //         return;
            //     }
            // }

            var validationResult = await _meisterschaftValidation.ValidateAsync(CurrentMeisterschaft);
            if (!validationResult.IsValid)
            {
                ValidationMessage = validationResult.ToString();
                return;
            }

            await _dbService.SaveMeisterschaftsdatenAsync(CurrentMeisterschaft);
            await LoadAndSetDataAsync();

            CurrentMeisterschaft = new();
            AreFieldsEditable = false;
            BtnNeuEnabled = true;
            BtnNeuVisibility = true;
            BtnAbbrechenEnabled = false;
            BtnBearbeitenEnabled = true;
            BtnSpeichernEnabled = false;
        }
    }
}