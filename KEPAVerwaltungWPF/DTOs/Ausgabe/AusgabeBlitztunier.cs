﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace KEPAVerwaltungWPF.DTOs.Ausgabe;

public partial class AusgabeBlitztunier : ObservableObject
{
    [ObservableProperty] private int iD;
    [ObservableProperty] private int spieltagID;
    [ObservableProperty] private DateTime spieltag;
    [ObservableProperty] private int spieler1ID;
    [ObservableProperty] private string spieler1Name;
    [ObservableProperty] private int spieler2ID;
    [ObservableProperty] private string spieler2Name;
    [ObservableProperty] private int punkteSpieler1;
    [ObservableProperty] private int punkteSpieler2;
    [ObservableProperty] private int hinRueckrunde;
}