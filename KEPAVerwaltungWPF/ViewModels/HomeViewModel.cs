using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
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
            
            ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Copyright = "(c) 2025 by Thorsten Schröer";
        }
        
        IsViewModelLoaded = true;
    }
    
    [ObservableProperty] private string programVersion = default;
    [ObservableProperty] private string copyright = default;
}