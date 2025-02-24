using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwStatistik9er
{
    public int MeisterschaftsId { get; set; }

    public string Bezeichnung { get; set; } = null!;

    public DateTime Beginn { get; set; }

    public DateTime? Ende { get; set; }

    public DateTime Spieltag { get; set; }

    public int SpielerId { get; set; }

    public int Neuner { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string? Spitzname { get; set; }
}
