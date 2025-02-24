using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Web;

public partial class TblSpieltag
{
    public int Id { get; set; }

    public int MeisterschaftsId { get; set; }

    public DateTime Spieltag { get; set; }

    public ulong InBearbeitung { get; set; }

    public virtual TblMeisterschaften Meisterschafts { get; set; } = null!;

    public virtual ICollection<Tbl9erRatten> Tbl9erRattens { get; set; } = new List<Tbl9erRatten>();

    public virtual ICollection<TblSpiel6TageRennen> TblSpiel6TageRennens { get; set; } = new List<TblSpiel6TageRennen>();

    public virtual ICollection<TblSpielBlitztunier> TblSpielBlitztuniers { get; set; } = new List<TblSpielBlitztunier>();

    public virtual ICollection<TblSpielMeisterschaft> TblSpielMeisterschafts { get; set; } = new List<TblSpielMeisterschaft>();

    public virtual ICollection<TblSpielPokal> TblSpielPokals { get; set; } = new List<TblSpielPokal>();

    public virtual ICollection<TblSpielSargKegeln> TblSpielSargKegelns { get; set; } = new List<TblSpielSargKegeln>();
}
