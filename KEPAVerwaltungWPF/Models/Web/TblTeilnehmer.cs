using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Web;

public partial class TblTeilnehmer
{
    public int Id { get; set; }

    public int MeisterschaftsId { get; set; }

    public int SpielerId { get; set; }

    public virtual TblMeisterschaften Meisterschafts { get; set; } = null!;

    public virtual TblMitglieder Spieler { get; set; } = null!;
}
