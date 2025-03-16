using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class StatistikViewModel : BaseViewModel
{
    public StatistikViewModel()
    {
        Titel = "Statistik";
    }
    
    [ObservableProperty] private bool zeitLaufendeMeisterschaft = true;
    [ObservableProperty] private bool zeitLetzteMeisterschaft = true;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ZeitBereichReadOnly))] private bool zeitBereich = false;
    [ObservableProperty] private bool zeitGesamt = false;
    
    [ObservableProperty] private bool auswahlNeuner = true;
    [ObservableProperty] private bool auswahlRatten = true;
    [ObservableProperty] private bool auswahlPokal = true;
    [ObservableProperty] private bool auswahlSarg = true;
    [ObservableProperty] private bool auswahlErgebnisseSpielerSpieler = true;
    [ObservableProperty] private bool auswahlPlatzierung6TageRennen = true;
    [ObservableProperty] private bool auswahlBesteMannschaft6TageREnnen = true;
    [ObservableProperty] private bool auswahlMannschaftMitglied6TageRennen = true;
    [ObservableProperty] private bool auswahlNeunerkoenigRattenorden = true;
    
    public bool ZeitBereichReadOnly => ZeitBereich;
    [ObservableProperty] DateTime? zeitBereichVon = null;
    [ObservableProperty] DateTime? zeitBereichBis = null;
}