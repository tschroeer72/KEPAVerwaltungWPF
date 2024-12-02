using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class StatistikSpielerErgebnisse : ObservableObject
{
    [ObservableProperty] private DateTime spieltag = DateTime.MinValue;
    [ObservableProperty] private string meisterschaft = string.Empty;
    [ObservableProperty] private string gegenspieler = string.Empty;
    [ObservableProperty] private int ergebnis = 0;
    [ObservableProperty] private int holz = 0;
    [ObservableProperty] private int sechsTageRennen_Runden = 0;
    [ObservableProperty] private int sechsTageRennen_Punkte = 0;
    [ObservableProperty] private long sechsTageRennen_Platz = 0;
    [ObservableProperty] private int sarg = 0;
    [ObservableProperty] private int pokal = 0;
    [ObservableProperty] private int neuner = 0;
    [ObservableProperty] private int ratten = 0;
}