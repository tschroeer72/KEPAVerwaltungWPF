using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs.Ausgabe;

public partial class AusgabeNeunerRatten : ObservableObject
{
    [ObservableProperty] private int meisterschaftsID = -1;
    [ObservableProperty] private int spieltagID = -1;
    [ObservableProperty] private DateTime spieltag = DateTime.MinValue;
    [ObservableProperty] private int spielerID = -1;
    [ObservableProperty] private string spielername = "";
    [ObservableProperty] private int neuner = 0;
    [ObservableProperty] private int ratten = 0;
}