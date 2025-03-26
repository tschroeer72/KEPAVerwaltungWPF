using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs.Statistik;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class StatistikViewModel : BaseViewModel
{
    private readonly DBService _dbService;
    private readonly PrintService _printService;

    public StatistikViewModel(DBService dbService, PrintService printService)
    {
        _dbService = dbService;
        _printService = printService;
        Titel = "Statistik";
    }
    
    [ObservableProperty] private bool zeitLaufendeMeisterschaft = true;
    [ObservableProperty] private bool zeitLetzteMeisterschaft = false;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ZeitBereichReadOnly))] private bool zeitBereich = false;
    [ObservableProperty] private bool zeitGesamt = false;
    
    [ObservableProperty] private bool auswahlNeuner = false;
    [ObservableProperty] private bool auswahlRatten = false;
    [ObservableProperty] private bool auswahlPokal = false;
    [ObservableProperty] private bool auswahlSarg = false;
    [ObservableProperty] private bool auswahlErgebnisseSpielerSpieler = false;
    [ObservableProperty] private bool auswahlPlatzierung6TageRennen = false;
    [ObservableProperty] private bool auswahlBesteMannschaft6TageRennen = false;
    [ObservableProperty] private bool auswahlMannschaftMitglied6TageRennen = true;
    [ObservableProperty] private bool auswahlNeunerkoenigRattenorden = false;
    
    public bool ZeitBereichReadOnly => ZeitBereich;
    [ObservableProperty] DateTime? zeitBereichVon = null;
    [ObservableProperty] DateTime? zeitBereichBis = null;

    [RelayCommand]
    public async Task VorschauAsync()
    {
        int intZeitbereich = -1;

        if (ZeitLaufendeMeisterschaft) intZeitbereich = 1;
        if (ZeitLetzteMeisterschaft) intZeitbereich = 2;
        if (ZeitBereich) intZeitbereich = 3;
        if (ZeitGesamt) intZeitbereich = 4;

        if (IsPageNotBusy)
        {
            IsPageBusy = true;
            
            if (AuswahlNeuner)
                await _printService.DruckStatistikNeunerAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlRatten)
                await _printService.DruckStatistikRattenAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlPokal)
                await _printService.DruckStatistikPokalAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlSarg)
                await _printService.DruckStatistikSargAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlErgebnisseSpielerSpieler)
                _printService.DruckStatistikSpielerSpielerAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlPlatzierung6TageRennen)
                await _printService.DruckStatistik6TageRennenPlatzAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlBesteMannschaft6TageRennen)
                await _printService.DruckStatistik6TageRennenBesteAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);

            if (AuswahlMannschaftMitglied6TageRennen)
                await _printService.DruckStatistik6TageRennenMannschaftAsync(intZeitbereich, ZeitBereichVon,
                    ZeitBereichBis);

            if (AuswahlNeunerkoenigRattenorden)
                await _printService.DruckStatistikNeunerRattenAsync(intZeitbereich, ZeitBereichVon, ZeitBereichBis);
            
            IsPageBusy = false;
        }
    }
}