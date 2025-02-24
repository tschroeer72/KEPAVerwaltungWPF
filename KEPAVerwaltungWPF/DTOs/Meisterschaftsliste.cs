using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class Meisterschaftsliste : ObservableObject
{
     [ObservableProperty] private int iD;
     [ObservableProperty] private string bezeichnung;
     [ObservableProperty] private System.DateTime beginn;
     [ObservableProperty] private Nullable<System.DateTime> ende;
     [ObservableProperty] private int meisterschaftstypID;
     [ObservableProperty] private string meisterschaftstyp;
     [ObservableProperty] private string bemerkungen;
     [ObservableProperty] private int aktiv;
}