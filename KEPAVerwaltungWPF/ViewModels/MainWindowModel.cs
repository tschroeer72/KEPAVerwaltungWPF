using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MainWindowModel : BaseViewModel  
{
    public MainWindowModel()
    {
        Titel = "Kegelgruppe KEPA 1958 Verwaltung";
    }

    [ObservableProperty] private double zoomFactor = 0.2;
    [ObservableProperty] private int zoomRadius = 200;
    [ObservableProperty] private bool zoomActive = false;

    [ObservableProperty] private string aktiveMeisterschaft = "keine aktive Meisterschaft";
    
    [RelayCommand]
    public void ShowSplashView()
    {
        DelGoBackOrGotoHome?.Invoke();
    }
}