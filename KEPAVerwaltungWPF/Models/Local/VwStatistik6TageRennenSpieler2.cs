using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwStatistik6TageRennenSpieler2
{
    public int MeisterschaftsId { get; set; }

    public int SpieltagId { get; set; }

    public DateTime Spieltag { get; set; }

    public int SpielerId { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string? Spitzname { get; set; }

    public int? Spielnummer { get; set; }

    public int Runden { get; set; }

    public int Punkte { get; set; }

    public long? Platz { get; set; }
}
