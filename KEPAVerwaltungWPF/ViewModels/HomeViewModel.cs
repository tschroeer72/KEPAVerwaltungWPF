using CommunityToolkit.Mvvm.Input;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public HomeViewModel()
    {
        Titel = "Home";
    }

    [RelayCommand]
    public  void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            //Datenbankabfrage
        }

        IsViewModelLoaded = true;
        
    }
}