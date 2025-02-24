using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class TblDbchangeLog
{
    public int Id { get; set; }

    public string? Computername { get; set; }

    public string? Tablename { get; set; }

    public string? Changetype { get; set; }

    public string? Command { get; set; }

    public DateTime? Zeitstempel { get; set; }
}
