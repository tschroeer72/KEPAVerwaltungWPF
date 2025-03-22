using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.DTOs.Ausgabe;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class ErgebnisuebersichtViewModel : BaseViewModel
{
    private readonly DBService _dbService;
    private readonly PrintService _printService;

    public ErgebnisuebersichtViewModel(DBService dbService, PrintService printService)
    {
        _dbService = dbService;
        _printService = printService;
        Titel = "Ergebnisübersicht";
    }
    
    
    private async Task LoadAndSetDataAsync()
    {
        var lstMeisterschaften = await _dbService.GetMeisterschaftenAsync();
        Meisterschaftsliste = new ObservableCollection<KeyValuePair<int, string>>(lstMeisterschaften.Select(x => new KeyValuePair<int, string>(x.ID, x.Bezeichnung)));
    }

    [ObservableProperty] private ObservableCollection<KeyValuePair<int, string>> meisterschaftsliste = new();
    [ObservableProperty] private int selectedMeisterschaftID = -1;
    [ObservableProperty] private string meisterschaftstyp = "";
    [ObservableProperty] private ObservableCollection<Spieltage> spieltageliste = new();
    [ObservableProperty] private ObservableCollection<Spieltage> selectedSpieltage = new();
    
    [ObservableProperty] private ObservableCollection<AusgabeNeunerRatten> ergebnisse9erRatten = new();
    [ObservableProperty] private ObservableCollection<AusgabeSechsTageRennen> ergebnisse6TageRennen = new();
    [ObservableProperty] private ObservableCollection<AusgabePokal> ergebnissePokal = new();
    [ObservableProperty] private ObservableCollection<AusgabeSargkegeln> ergebnisseSargkegeln = new();
    [ObservableProperty] private ObservableCollection<AusgabeMeisterschaft> ergebnisseMeisterschaft = new();
    [ObservableProperty] private ObservableCollection<AusgabeBlitztunier> ergebnisseBlitztunier = new();
    [ObservableProperty] private ObservableCollection<AusgabeKombimeisterschaft> ergebnisseKombimeisterschaft = new();
    
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
    public async Task SelectedMeisterschaftChangedAsync()
    {
        Meisterschaftstyp = await _dbService.GetMeisterschaftstypFromMeisterschaftByIDAsync(SelectedMeisterschaftID);
        
        Spieltageliste.Clear();
        Spieltageliste = new ObservableCollection<Spieltage>(await _dbService.GetSpieltagListAsync(SelectedMeisterschaftID));
        
        SelectedSpieltage.Clear();
    }
    
    [RelayCommand]
    public void SelectAllDays()
    {
        SelectedSpieltage = new ObservableCollection<Spieltage>(Spieltageliste);
    
        // // Manuelle Aktualisierung der ListBox-Selektion
        // var listBox = Application.Current.MainWindow.FindName("lbSpieltage") as ListBox;
        // if (listBox != null)
        // {
        //     listBox.SelectedItems.Clear();
        //     foreach (var item in SelectedSpieltage)
        //     {
        //         listBox.SelectedItems.Add(item);
        //     }
        // }
    }
    
    [RelayCommand]
    public async Task RefreshData()
    {
        var lstSpieltageIDs = SelectedSpieltage.Select(s => s.Id).ToList();
        
        Ergebnisse9erRatten = new ObservableCollection<AusgabeNeunerRatten>(await _dbService.Get9erRattenFromSpieltageAsync(lstSpieltageIDs));
        Ergebnisse6TageRennen = new ObservableCollection<AusgabeSechsTageRennen>(await _dbService.Get6TageRennenFromSpieltageAsync(lstSpieltageIDs));
        ErgebnissePokal = new ObservableCollection<AusgabePokal>(await _dbService.GetPokalFromSpieltageAsync(lstSpieltageIDs));
        ErgebnisseMeisterschaft = new ObservableCollection<AusgabeMeisterschaft>(await _dbService.GetMeisterschaftFromSpieltageAsync(lstSpieltageIDs));
        ErgebnisseKombimeisterschaft = new ObservableCollection<AusgabeKombimeisterschaft>(await _dbService.GetKombimeisterschaftFromSpieltageAsync(lstSpieltageIDs));
        ErgebnisseBlitztunier = new ObservableCollection<AusgabeBlitztunier>(await _dbService.GetBlitztunierFromSpieltageAsync(lstSpieltageIDs));
    }

    [RelayCommand]
    public async Task DruckvorschauAsync()
    {
        switch (Meisterschaftstyp)
        {
            case "Kurzturnier":
                await _printService.DruckErgebnisKurztunierAsync(SelectedMeisterschaftID);
                break;
            case "Meisterschaft":
                await _printService.DruckErgebnisMeisterschaftAsync(SelectedMeisterschaftID);
                break;
            case "Kombimeisterschaft":
                await _printService.DruckErgebnisKombimeisterschaftAsync(SelectedMeisterschaftID);
                break;
        }
    }
}
