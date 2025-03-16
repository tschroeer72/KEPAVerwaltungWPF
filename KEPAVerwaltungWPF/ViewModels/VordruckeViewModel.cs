using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.Services;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class VordruckeViewModel : BaseViewModel
{
    private readonly DBService _dbService;

    public VordruckeViewModel(DBService dbService)
    {
        _dbService = dbService;
        Titel = "Listen und Vordrucke";
    }

    [ObservableProperty] private bool auswahlAktiveMitglieder = true;
    [ObservableProperty] private bool auswahlAlleMitglieder = false;
    
    [ObservableProperty] private bool auswahl6TageRennen = false;
    [ObservableProperty] private bool auswahlKombimeisterschaft = false;
    [ObservableProperty] private bool auswahlMeisterschaft = false;
    [ObservableProperty] private bool auswahlBlitztunier = false;
    [ObservableProperty] private bool auswahlWeihnachtsbaum = false;
    
    [ObservableProperty] private bool auswahlSpielverluste = false;
    [ObservableProperty] private bool auswahlAbrechnung = false;
    
    [RelayCommand]
    public void Vorschau()
    {
        
    }
}