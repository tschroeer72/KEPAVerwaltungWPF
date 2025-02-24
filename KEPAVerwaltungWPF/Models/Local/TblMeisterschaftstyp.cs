using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class TblMeisterschaftstyp
{
    public int Id { get; set; }

    public string Meisterschaftstyp { get; set; } = null!;

    public virtual ICollection<TblMeisterschaften> TblMeisterschaftens { get; set; } = new List<TblMeisterschaften>();
}
