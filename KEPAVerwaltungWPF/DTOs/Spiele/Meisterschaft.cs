using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class Meisterschaft : ObservableObject
{
    [ObservableProperty] private int iD;
    [ObservableProperty] private int spieltagID;
    [ObservableProperty] private int spieler1ID;
    [ObservableProperty] private string spieler1Name;
    [ObservableProperty] private int spieler2ID;
    [ObservableProperty] private string spieler2Name;
    [ObservableProperty] private int holzSpieler1;
    [ObservableProperty] private int holzSpieler2;
    [ObservableProperty] private int hinRueckrunde;
}