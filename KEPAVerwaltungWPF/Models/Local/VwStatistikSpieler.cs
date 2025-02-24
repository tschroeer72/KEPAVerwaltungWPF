using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwStatistikSpieler
{
    public int MeisterschaftsId { get; set; }

    public string Bezeichnung { get; set; } = null!;

    public string Meisterschaftstyp { get; set; } = null!;

    public DateTime Spieltag { get; set; }

    public int SpielerId1 { get; set; }

    public string Spieler1 { get; set; } = null!;

    public int HolzSpieler1 { get; set; }

    public int SpielerId2 { get; set; }

    public string Spieler2 { get; set; } = null!;

    public int HolzSpieler2 { get; set; }
}
