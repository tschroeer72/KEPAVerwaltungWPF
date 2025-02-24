using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwErgebnisKurztunier
{
    public int MeisterschaftsId { get; set; }

    public int? Jahr { get; set; }

    public int SpielerId1 { get; set; }

    public string Spieler1Vorname { get; set; } = null!;

    public string Spieler1Nachname { get; set; } = null!;

    public string? Spieler1Spitzname { get; set; }

    public int SpielerId2 { get; set; }

    public string Spieler2Vorname { get; set; } = null!;

    public string Spieler2Nachname { get; set; } = null!;

    public string? Spieler2Spitzname { get; set; }

    public int PunkteSpieler1 { get; set; }

    public int PunkteSpieler2 { get; set; }

    public int HinRückrunde { get; set; }
}
