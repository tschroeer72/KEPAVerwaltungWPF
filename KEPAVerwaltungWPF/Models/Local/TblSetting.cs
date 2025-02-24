using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class TblSetting
{
    public int Id { get; set; }

    public string Computername { get; set; } = null!;

    public string Parametername { get; set; } = null!;

    public string Parameterwert { get; set; } = null!;
}
