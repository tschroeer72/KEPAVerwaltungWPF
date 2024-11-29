using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class Spieltage : ObservableObject
{
    [ObservableProperty] private bool isSelected = false;
    [ObservableProperty] private DateTime spieltag;
}