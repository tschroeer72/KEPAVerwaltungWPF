using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.Enums;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MainWindowModel : BaseViewModel  
{
    public MainWindowModel()
    {
        Titel = "Kegelgruppe KEPA 1958 Verwaltung";
    }

    [ObservableProperty] private double zoomFactor = 0.2;
    [ObservableProperty] private double zoomRadius = 100;
    [ObservableProperty] private bool zoomActive = false;
    [ObservableProperty] private MagnifyType zoomType = MagnifyType.Rectangle;

    [ObservableProperty] private string aktiveMeisterschaft = "keine aktive Meisterschaft";
    
    [RelayCommand]
    public void ShowSplashView()
    {
        DelGoBackOrGotoHome?.Invoke();
    }
    
    
}