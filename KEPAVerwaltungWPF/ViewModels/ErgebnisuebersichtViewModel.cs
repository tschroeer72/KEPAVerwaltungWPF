using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class ErgebnisuebersichtViewModel : BaseViewModel
{
    public ErgebnisuebersichtViewModel()
    {
        Titel = "Ergebnisübersicht";
    }
    
    
    private void LoadAndSetData()
    {
        DateTime dtTemp = new DateTime(2024, 1, 6);
        for (int i = 0; i < 10; i++)
        {
            Spieltage objSpieltag = new();
            objSpieltag.Spieltag = dtTemp; 
            LstSpieltage.Add(objSpieltag);
            dtTemp = dtTemp.AddDays(14);
        }
    }

    [ObservableProperty] private List<Spieltage> lstSpieltage = new();
    
    [RelayCommand]
    public void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            LoadAndSetData();
            IsViewModelLoaded = true;
        }
    }
}
