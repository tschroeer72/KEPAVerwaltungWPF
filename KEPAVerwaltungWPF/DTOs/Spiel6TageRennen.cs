using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class Spiel6TageRennen : ObservableObject
{
     [ObservableProperty] private int iD;
     [ObservableProperty] private int spieltagID;
     [ObservableProperty] private int spieler1ID;
     [ObservableProperty] private string spieler1Name;
     [ObservableProperty] private int spieler2ID;
     [ObservableProperty] private string spieler2Name;
     [ObservableProperty] private int runden;
     [ObservableProperty] private int punkte;
     [ObservableProperty] private int spielnummer;
     [ObservableProperty] private Int64 platz;
}