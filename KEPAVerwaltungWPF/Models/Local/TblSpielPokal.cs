﻿using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class TblSpielPokal
{
    public int Id { get; set; }

    public int SpieltagId { get; set; }

    public int SpielerId { get; set; }

    public int Platzierung { get; set; }

    public virtual TblMitglieder Spieler { get; set; } = null!;

    public virtual TblSpieltag Spieltag { get; set; } = null!;
}
