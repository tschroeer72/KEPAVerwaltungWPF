using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwStatistik9erRatten
{
    public int MeisterschaftsId { get; set; }

    public string Bezeichnung { get; set; } = null!;

    public DateTime Beginn { get; set; }

    public DateTime? Ende { get; set; }

    public string Meisterschaftstyp { get; set; } = null!;

    public DateTime Spieltag { get; set; }

    public int Id { get; set; }

    public int SpielerIdneunerkönig { get; set; }

    public int SpielerIdrattenorden { get; set; }
}
