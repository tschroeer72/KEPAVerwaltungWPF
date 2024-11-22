using CommunityToolkit.Mvvm.Input;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MainWindowModel : BaseViewModel  
{
    public MainWindowModel()
    {
        Titel = "Kegelgruppe KEPA 1958 Verwaltung";
    }
    
    [RelayCommand]
    public void ShowSplashView()
    {
        DelGoBackOrGotoHome?.Invoke();
    }
}