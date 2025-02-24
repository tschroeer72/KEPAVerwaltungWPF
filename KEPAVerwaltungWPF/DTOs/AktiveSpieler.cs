using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class AktiveSpieler : ObservableObject
{
     [ObservableProperty] private int iD;
     [ObservableProperty] private string anzeigename;
     [ObservableProperty] private string nachname;
     [ObservableProperty] private string vorname;
     [ObservableProperty] private string spitzname;
}