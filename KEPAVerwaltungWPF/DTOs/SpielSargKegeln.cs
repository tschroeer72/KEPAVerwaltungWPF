﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs;

public partial class SpielSargKegeln : ObservableObject
{
    [ObservableProperty] private int iD;
    [ObservableProperty] private int spieltagID;
    [ObservableProperty] private int spielerID;
    [ObservableProperty] private string spielername;
    [ObservableProperty] private int platzierung;
}