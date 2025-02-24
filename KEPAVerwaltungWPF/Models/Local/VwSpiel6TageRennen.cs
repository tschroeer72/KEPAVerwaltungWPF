using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwSpiel6TageRennen
{
    public int MeisterschaftsId { get; set; }

    public int _6tageRennenId { get; set; }

    public int SpieltagId { get; set; }

    public DateTime Spieltag { get; set; }

    public int SpielerId1 { get; set; }

    public string Spieler1Vorname { get; set; } = null!;

    public string Spieler1Nachname { get; set; } = null!;

    public string? Spieler1Spitzname { get; set; }

    public int SpielerId2 { get; set; }

    public string Spieler2Vorname { get; set; } = null!;

    public string Spieler2Nachname { get; set; } = null!;

    public string? Spieler2Spitzname { get; set; }

    public int Runden { get; set; }

    public int Punkte { get; set; }

    public int? Spielnummer { get; set; }

    public long? Platz { get; set; }
}
