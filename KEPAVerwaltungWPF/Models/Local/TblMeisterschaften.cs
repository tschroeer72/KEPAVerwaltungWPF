using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class TblMeisterschaften
{
    public int Id { get; set; }

    public string Bezeichnung { get; set; } = null!;

    public DateTime Beginn { get; set; }

    public DateTime? Ende { get; set; }

    public int MeisterschaftstypId { get; set; }

    public int? TurboDbnummer { get; set; }

    public int Aktiv { get; set; }

    public string? Bemerkungen { get; set; }

    public virtual TblMeisterschaftstyp Meisterschaftstyp { get; set; } = null!;

    public virtual ICollection<TblSpieltag> TblSpieltags { get; set; } = new List<TblSpieltag>();

    public virtual ICollection<TblTeilnehmer> TblTeilnehmers { get; set; } = new List<TblTeilnehmer>();
}
