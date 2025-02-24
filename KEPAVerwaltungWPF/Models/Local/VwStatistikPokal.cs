using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class VwStatistikPokal
{
    public int MeisterschaftsId { get; set; }

    public int Id { get; set; }

    public DateTime Spieltag { get; set; }

    public int SpielerId { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string? Spitzname { get; set; }

    public int Platzierung { get; set; }
}
