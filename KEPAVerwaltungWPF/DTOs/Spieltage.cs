using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class Spieltage : ObservableObject
{
    [ObservableProperty] private int id = -1;
    [ObservableProperty] private DateTime spieltag;
}