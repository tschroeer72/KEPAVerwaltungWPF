using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class Vw9erRatten
{
    public int MeisterschaftsId { get; set; }

    public int SpieltagId { get; set; }

    public DateTime Spieltag { get; set; }

    public int _9erRattenId { get; set; }

    public int SpielerId { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string? Spitzname { get; set; }

    public int Neuner { get; set; }

    public int Ratten { get; set; }
}
