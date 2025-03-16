using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class StatistikViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public StatistikViewModel(DBService dbService)
    {
        _dbService = dbService;
        Titel = "Statistik";
    }
    
    [ObservableProperty] private bool zeitLaufendeMeisterschaft = true;
    [ObservableProperty] private bool zeitLetzteMeisterschaft = false;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ZeitBereichReadOnly))] private bool zeitBereich = false;
    [ObservableProperty] private bool zeitGesamt = false;
    
    [ObservableProperty] private bool auswahlNeuner = true;
    [ObservableProperty] private bool auswahlRatten = false;
    [ObservableProperty] private bool auswahlPokal = false;
    [ObservableProperty] private bool auswahlSarg = false;
    [ObservableProperty] private bool auswahlErgebnisseSpielerSpieler = false;
    [ObservableProperty] private bool auswahlPlatzierung6TageRennen = false;
    [ObservableProperty] private bool auswahlBesteMannschaft6TageREnnen = false;
    [ObservableProperty] private bool auswahlMannschaftMitglied6TageRennen = false;
    [ObservableProperty] private bool auswahlNeunerkoenigRattenorden = false;
    
    public bool ZeitBereichReadOnly => ZeitBereich;
    [ObservableProperty] DateTime? zeitBereichVon = null;
    [ObservableProperty] DateTime? zeitBereichBis = null;

    [RelayCommand]
    public void Vorschau()
    {
        
    }
}