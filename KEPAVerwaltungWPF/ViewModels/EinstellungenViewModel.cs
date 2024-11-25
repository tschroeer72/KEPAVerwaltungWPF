using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class EinstellungenViewModel : BaseViewModel
{
    public MainWindowModel MainWindowModel { get; }

    // public EinstellungenViewModel()
    // {
    //     Titel = "Einstellungen";
    // }
    public EinstellungenViewModel(MainWindowModel mainWindowModel)
    {
        MainWindowModel = mainWindowModel;
        Titel = "Einstellungen";
    }

    public void ZoomActive(bool isActive)
    {
        if (isActive) ZoomFactor = 0.2;
        else ZoomFactor = 0;
    }
    
    [ObservableProperty] private double zoomFactor = 0.2;
    [ObservableProperty] private double zoomRadius = 100.0;

    [RelayCommand]
    public void ZoomFactorChanged()
    {
        MainWindowModel.ZoomFactor = ZoomFactor;
    }

    [RelayCommand]
    public void ZoomRadiusChanged()
    {
        MainWindowModel.ZoomRadius = (int)ZoomRadius;
    }

}