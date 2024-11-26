using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.Enums;

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

    public void ZoomTypeChanged(MagnifyType magnifyType)
    {
        MainWindowModel.ZoomType = magnifyType;
        Properties.Settings.Default.ZoomType = magnifyType.ToString();
        Properties.Settings.Default.Save();
    }
    
    [ObservableProperty] private double zoomFactor = 0.2;
    [ObservableProperty] private double zoomRadius = 100.0;
    [ObservableProperty] private MagnifyType zoomType = MagnifyType.Circle;
    [ObservableProperty] private bool zoomTypeIsCircle = true;
    [ObservableProperty] private bool zoomTypeIsRectangle= false;
    
    [RelayCommand]
    public void LoadProperties()
    {
        ZoomFactor = Properties.Settings.Default.ZoomFactor;
        ZoomRadius = Properties.Settings.Default.ZoomRadius;
        switch (Properties.Settings.Default.ZoomType)
        {
            case "Circle":
                ZoomType = MagnifyType.Circle;
                ZoomTypeIsCircle = true;
                ZoomTypeIsRectangle = false;
                break;
            case "Rectangle":
                ZoomType = MagnifyType.Rectangle;
                ZoomTypeIsCircle = false;
                ZoomTypeIsRectangle = true;
                break;
        }
    }
    
    [RelayCommand]
    public void ZoomFactorChanged()
    {
        MainWindowModel.ZoomFactor = ZoomFactor;
        Properties.Settings.Default.ZoomFactor = MainWindowModel.ZoomFactor;
        Properties.Settings.Default.Save();
    }

    [RelayCommand]
    public void ZoomRadiusChanged()
    {
        MainWindowModel.ZoomRadius = ZoomRadius;
        Properties.Settings.Default.ZoomRadius = MainWindowModel.ZoomRadius;
        Properties.Settings.Default.Save();
    }

}