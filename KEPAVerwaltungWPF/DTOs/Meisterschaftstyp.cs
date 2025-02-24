using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class Meisterschaftstyp : ObservableObject
{
     [ObservableProperty] private int iD;
     [ObservableProperty] private string value;
}