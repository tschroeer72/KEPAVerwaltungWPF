using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KEPAVerwaltungWPF.DTOs;
using KEPAVerwaltungWPF.Enums;
using KEPAVerwaltungWPF.Services;
using PdfSharp.Quality;

namespace KEPAVerwaltungWPF.ViewModels;

public partial class MainWindowModel : BaseViewModel  
{
    public CommonService CommonService { get; }

    public AktiveMeisterschaft MeisterschaftAktiv => CommonService.AktiveMeisterschaft;
    public MainWindowModel(CommonService commonService)
    {
        CommonService = commonService;
        CommonService.OnChange += () => OnPropertyChanged(nameof(MeisterschaftAktiv));
        Titel = "Kegelgruppe KEPA 1958 Verwaltung";
    }

    [ObservableProperty] private double zoomFactor = 0.2;
    [ObservableProperty] private double zoomRadius = 100;
    [ObservableProperty] private bool zoomActive = false;
    [ObservableProperty] private MagnifyType zoomType = MagnifyType.Rectangle;
    
    // [ObservableProperty] private string aktiveMeisterschaft = "keine aktive Meisterschaft";

}