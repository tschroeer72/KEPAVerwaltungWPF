using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Web;

public partial class Tbl9erRatten
{
    public int Id { get; set; }

    public int SpieltagId { get; set; }

    public int SpielerId { get; set; }

    public int Neuner { get; set; }

    public int Ratten { get; set; }

    public virtual TblMitglieder Spieler { get; set; } = null!;

    public virtual TblSpieltag Spieltag { get; set; } = null!;
}
