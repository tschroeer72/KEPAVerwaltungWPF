using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class StatistikSpieler : ObservableObject
{
    [ObservableProperty] private int holzMeisterSumme;
    [ObservableProperty] private int holzMeisterMax;
    [ObservableProperty] private int holzMeisterMin;
    [ObservableProperty] private double holzMeisterAVG;
    [ObservableProperty] private int holzBlitzSumme;
    [ObservableProperty] private int holzBlitzMax;
    [ObservableProperty] private int holzBlitzMin;
    [ObservableProperty] private double holzBlitzAVG;
    [ObservableProperty] private int holzSumme;
    [ObservableProperty] private int holzMax;
    [ObservableProperty] private int holzMin;
    [ObservableProperty] private double holzAVG;
    [ObservableProperty] private int rattenSumme;
    [ObservableProperty] private int rattenMax;
    [ObservableProperty] private int neunerSumme;
    [ObservableProperty] private int neunerMax;
}