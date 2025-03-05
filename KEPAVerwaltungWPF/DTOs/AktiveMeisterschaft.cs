using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class AktiveMeisterschaft : ObservableObject
{
    [ObservableProperty] private int iD = -1;
    [ObservableProperty] private string bezeichnung = "keine aktive Meisterschaft";
}