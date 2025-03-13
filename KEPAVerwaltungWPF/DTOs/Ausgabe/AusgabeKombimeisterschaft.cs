using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs.Ausgabe;

public partial class AusgabeKombimeisterschaft: ObservableObject
{
    [ObservableProperty] private int iD;
    [ObservableProperty] private int spieltagID;
    [ObservableProperty] private DateTime spieltag;
    [ObservableProperty] private int spieler1ID;
    [ObservableProperty] private string spieler1Name;
    [ObservableProperty] private int spieler2ID;
    [ObservableProperty] private string spieler2Name;
    [ObservableProperty] private int spieler1Punkte3bis8;
    [ObservableProperty] private int spieler1Punkte5Kugeln;
    [ObservableProperty] private int spieler2Punkte3bis8;
    [ObservableProperty] private int spieler2Punkte5Kugeln;
    [ObservableProperty] private int hinRueckrunde;
}