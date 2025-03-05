using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.Models.Local;
using KEPAVerwaltungWPF.Services;
using KEPAVerwaltungWPF.Views;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public HomeViewModel(DBService dbService)
    {
        Titel = "Home";
        _dbService = dbService;
    }

    [RelayCommand]
    public async void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            //Datenbankabfrage
            //await _dbService.UpdateLocalDBAsync();
        }
        
        IsViewModelLoaded = true;
    }
}