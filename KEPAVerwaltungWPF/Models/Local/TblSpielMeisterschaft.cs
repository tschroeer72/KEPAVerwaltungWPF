using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class TblSpielMeisterschaft
{
    public int Id { get; set; }

    public int SpieltagId { get; set; }

    public int SpielerId1 { get; set; }

    public int SpielerId2 { get; set; }

    public int HolzSpieler1 { get; set; }

    public int HolzSpieler2 { get; set; }

    public int HinRückrunde { get; set; }

    public virtual TblMitglieder SpielerId1Navigation { get; set; } = null!;

    public virtual TblMitglieder SpielerId2Navigation { get; set; } = null!;

    public virtual TblSpieltag Spieltag { get; set; } = null!;
}
